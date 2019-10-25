using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Uwl.Extends.Utility;

namespace Uwl.Data.EntityFramework.EFExtensionsSql
{
    public static class UtilsExpressionTree
    {
        private static Dictionary<Type, bool> dicExecuteArrayRowReadClassOrTuple = new Dictionary<Type, bool>
        {
            [typeof(bool)] = true,
            [typeof(sbyte)] = true,
            [typeof(short)] = true,
            [typeof(int)] = true,
            [typeof(long)] = true,
            [typeof(byte)] = true,
            [typeof(ushort)] = true,
            [typeof(uint)] = true,
            [typeof(ulong)] = true,
            [typeof(double)] = true,
            [typeof(float)] = true,
            [typeof(decimal)] = true,
            [typeof(TimeSpan)] = true,
            [typeof(DateTime)] = true,
            [typeof(DateTimeOffset)] = true,
            [typeof(byte[])] = true,
            [typeof(string)] = true,
            [typeof(Guid)] = true,
            //[typeof(MygisPoint)] = true,
            //[typeof(MygisLineString)] = true,
            //[typeof(MygisPolygon)] = true,
            //[typeof(MygisMultiPoint)] = true,
            //[typeof(MygisMultiLineString)] = true,
            //[typeof(MygisMultiPolygon)] = true,
            //[typeof(BitArray)] = true,
            //[typeof(NpgsqlPoint)] = true,
            //[typeof(NpgsqlLine)] = true,
            //[typeof(NpgsqlLSeg)] = true,
            //[typeof(NpgsqlBox)] = true,
            //[typeof(NpgsqlPath)] = true,
            //[typeof(NpgsqlPolygon)] = true,
            //[typeof(NpgsqlCircle)] = true,
            //[typeof((IPAddress Address, int Subnet))] = true,
            //[typeof(IPAddress)] = true,
            //[typeof(PhysicalAddress)] = true,
            //[typeof(NpgsqlRange<int>)] = true,
            //[typeof(NpgsqlRange<long>)] = true,
            //[typeof(NpgsqlRange<decimal>)] = true,
            //[typeof(NpgsqlRange<DateTime>)] = true,
            //[typeof(PostgisPoint)] = true,
            //[typeof(PostgisLineString)] = true,
            //[typeof(PostgisPolygon)] = true,
            //[typeof(PostgisMultiPoint)] = true,
            //[typeof(PostgisMultiLineString)] = true,
            //[typeof(PostgisMultiPolygon)] = true,
            //[typeof(PostgisGeometry)] = true,
            //[typeof(PostgisGeometryCollection)] = true,
            [typeof(Dictionary<string, string>)] = true,
            [typeof(JToken)] = true,
            [typeof(JObject)] = true,
            [typeof(JArray)] = true,
        };
        private static ConcurrentDictionary<Type, Func<Type, int[], DbDataReader, int, RowInfo>> _dicExecuteArrayRowReadClassOrTuple = new ConcurrentDictionary<Type, Func<Type, int[], DbDataReader, int, RowInfo>>();
        public class RowInfo
        {
            public object Value { get; set; }
            public int DataIndex { get; set; }
            public RowInfo(object value, int dataIndex)
            {
                this.Value = value;
                this.DataIndex = dataIndex;
            }
            public static ConstructorInfo Constructor = typeof(RowInfo).GetConstructor(new[] { typeof(object), typeof(int) });
            public static PropertyInfo PropertyValue = typeof(RowInfo).GetProperty("Value");
            public static PropertyInfo PropertyDataIndex = typeof(RowInfo).GetProperty("DataIndex");
        }
        private static MethodInfo MethodDataReaderGetValue = typeof(DbDataReader).GetMethod("GetValue");
        public static RowInfo ExecuteArrayRowReadClassOrTuple(Type type, int[] indexes, DbDataReader row, int dataIndex = 0)
        {
            var func = _dicExecuteArrayRowReadClassOrTuple.GetOrAdd(type, s =>
            {
                var returnTarget = Expression.Label(typeof(RowInfo));
                var typeExp = Expression.Parameter(typeof(Type), "type");
                var indexesExp = Expression.Parameter(typeof(int[]), "indexes");
                var rowExp = Expression.Parameter(typeof(DbDataReader), "row");
                var dataIndexExp = Expression.Parameter(typeof(int), "dataIndex");

                if (type.IsArray) return Expression.Lambda<Func<Type, int[], DbDataReader, int, RowInfo>>(
                    Expression.New(RowInfo.Constructor,
                        GetDataReaderValueBlockExpression(type, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),

                        Expression.Add(dataIndexExp, Expression.Constant(1))
                    ), new[] { typeExp, indexesExp, rowExp, dataIndexExp }).Compile();

                var typeGeneric = type;
                if (typeGeneric.IsNullableType()) typeGeneric = type.GenericTypeArguments.First();
                if (typeGeneric.IsEnum ||
                    dicExecuteArrayRowReadClassOrTuple.ContainsKey(typeGeneric))
                    return Expression.Lambda<Func<Type, int[], DbDataReader, int, RowInfo>>(
                    Expression.New(RowInfo.Constructor,
                        GetDataReaderValueBlockExpression(type, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),

                        Expression.Add(dataIndexExp, Expression.Constant(1))
                    ), new[] { typeExp, indexesExp, rowExp, dataIndexExp }).Compile();

                if (type.Namespace == "System" && (type.FullName == "System.String" || type.IsValueType))
                { //值类型，或者元组
                    bool isTuple = type.Name.StartsWith("ValueTuple`");
                    if (isTuple)
                    {
                        var ret2Exp = Expression.Variable(type, "ret");
                        var read2Exp = Expression.Variable(typeof(RowInfo), "read");
                        var read2ExpValue = Expression.MakeMemberAccess(read2Exp, RowInfo.PropertyValue);
                        var read2ExpDataIndex = Expression.MakeMemberAccess(read2Exp, RowInfo.PropertyDataIndex);
                        var block2Exp = new List<Expression>();

                        var fields = type.GetFields();
                        foreach (var field in fields)
                        {
                            Expression read2ExpAssign = null; //加速缓存
                            if (field.FieldType.IsArray) read2ExpAssign = Expression.New(RowInfo.Constructor,
                                GetDataReaderValueBlockExpression(field.FieldType, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),

                                Expression.Add(dataIndexExp, Expression.Constant(1))
                            );
                            else
                            {
                                var fieldtypeGeneric = field.FieldType;
                                //if (fieldtypeGeneric.IsNullableType())
                                    fieldtypeGeneric = fieldtypeGeneric.GenericTypeArguments.First();
                                if (fieldtypeGeneric.IsEnum ||
                                    dicExecuteArrayRowReadClassOrTuple.ContainsKey(fieldtypeGeneric)) read2ExpAssign = Expression.New(RowInfo.Constructor,
                                        GetDataReaderValueBlockExpression(field.FieldType, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),
                                        //Expression.Call(MethodGetDataReaderValue, new Expression[] { Expression.Constant(field.FieldType), Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp) }),
                                        Expression.Add(dataIndexExp, Expression.Constant(1))
                                );
                                else
                                {
                                    read2ExpAssign = Expression.Call(MethodExecuteArrayRowReadClassOrTuple, new Expression[] { Expression.Constant(field.FieldType), indexesExp, rowExp, dataIndexExp });
                                }
                            }
                            block2Exp.AddRange(new Expression[] {
								//Expression.TryCatch(Expression.Block(
								//	typeof(void),
									Expression.Assign(read2Exp, read2ExpAssign),
                                    Expression.IfThen(Expression.GreaterThan(read2ExpDataIndex, dataIndexExp),
                                        Expression.Assign(dataIndexExp, read2ExpDataIndex)),
                                    Expression.IfThenElse(Expression.Equal(read2ExpValue, Expression.Constant(null)),
                                        Expression.Assign(Expression.MakeMemberAccess(ret2Exp, field), Expression.Default(field.FieldType)),
                                        Expression.Assign(Expression.MakeMemberAccess(ret2Exp, field), Expression.Convert(read2ExpValue, field.FieldType)))
								//), 
								//Expression.Catch(typeof(Exception), Expression.Block(
								//		Expression.IfThen(Expression.Equal(read2ExpDataIndex, Expression.Constant(0)), Expression.Throw(Expression.Constant(new Exception(field.Name + "," + 0)))),
								//		Expression.IfThen(Expression.Equal(read2ExpDataIndex, Expression.Constant(1)), Expression.Throw(Expression.Constant(new Exception(field.Name + "," + 1)))),
								//		Expression.IfThen(Expression.Equal(read2ExpDataIndex, Expression.Constant(2)), Expression.Throw(Expression.Constant(new Exception(field.Name + "," + 2)))),
								//		Expression.IfThen(Expression.Equal(read2ExpDataIndex, Expression.Constant(3)), Expression.Throw(Expression.Constant(new Exception(field.Name + "," + 3)))),
								//		Expression.IfThen(Expression.Equal(read2ExpDataIndex, Expression.Constant(4)), Expression.Throw(Expression.Constant(new Exception(field.Name + "," + 4))))
								//	)
								//))
							});
                        }
                        block2Exp.AddRange(new Expression[] {
                            Expression.Return(returnTarget, Expression.New(RowInfo.Constructor, Expression.Convert(ret2Exp, typeof(object)), dataIndexExp)),
                            Expression.Label(returnTarget, Expression.Default(typeof(RowInfo)))
                        });
                        return Expression.Lambda<Func<Type, int[], DbDataReader, int, RowInfo>>(
                            Expression.Block(new[] { ret2Exp, read2Exp }, block2Exp), new[] { typeExp, indexesExp, rowExp, dataIndexExp }).Compile();
                    }
                    var rowLenExp = Expression.ArrayLength(rowExp);
                    return Expression.Lambda<Func<Type, int[], DbDataReader, int, RowInfo>>(
                        Expression.Block(
                            Expression.IfThen(
                                Expression.LessThan(dataIndexExp, rowLenExp),
                                    Expression.Return(returnTarget, Expression.New(RowInfo.Constructor,
                                        GetDataReaderValueBlockExpression(type, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),
                                        //Expression.Call(MethodGetDataReaderValue, new Expression[] { typeExp, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp) }),
                                        Expression.Add(dataIndexExp, Expression.Constant(1))))
                            ),
                            Expression.Label(returnTarget, Expression.Default(typeof(RowInfo)))
                        ), new[] { typeExp, indexesExp, rowExp, dataIndexExp }).Compile();
                }

                if (type == typeof(object) && indexes != null)
                {
                    Func<Type, int[], DbDataReader, int, RowInfo> dynamicFunc = (type2, indexes2, row2, dataindex2) =>
                    {
                        dynamic expando = new System.Dynamic.ExpandoObject(); //动态类型字段 可读可写
                        var expandodic = (IDictionary<string, object>)expando;
                        var fc = row2.FieldCount;
                        for (var a = 0; a < fc; a++)
                            expandodic.Add(row2.GetName(a), row2.GetValue(a));
                        return new RowInfo(expando, fc);
                    };
                    return dynamicFunc;// Expression.Lambda<Func<Type, int[], DbDataReader, int, RowInfo>>(null);
                }

                //类注入属性
                var retExp = Expression.Variable(type, "ret");
                var readExp = Expression.Variable(typeof(RowInfo), "read");
                var readExpValue = Expression.MakeMemberAccess(readExp, RowInfo.PropertyValue);
                var readExpDataIndex = Expression.MakeMemberAccess(readExp, RowInfo.PropertyDataIndex);
                var readExpValueParms = new List<ParameterExpression>();
                var readExpsIndex = Expression.Variable(typeof(int), "readsIndex");
                var tryidxExp = Expression.Variable(typeof(int), "tryidx");
                var indexesLengthExp = Expression.Parameter(typeof(int), "indexesLength");
                var blockExp = new List<Expression>();
                var ctor = type.GetConstructor(new Type[0]) ?? type.GetConstructors().First();
                var ctorParms = ctor.GetParameters();
                if (ctorParms.Length > 0)
                {
                    foreach (var ctorParm in ctorParms)
                    {
                        Expression readExpAssign = null; //加速缓存
                        if (ctorParm.ParameterType.IsArray) readExpAssign = Expression.New(RowInfo.Constructor,
                            GetDataReaderValueBlockExpression(ctorParm.ParameterType, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),
                            //Expression.Call(MethodGetDataReaderValue, new Expression[] { Expression.Constant(ctorParm.ParameterType), Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp) }),
                            Expression.Add(dataIndexExp, Expression.Constant(1))
                        );
                        else
                        {
                            var proptypeGeneric = ctorParm.ParameterType;
                            if (proptypeGeneric.IsNullableType()) proptypeGeneric = proptypeGeneric.GenericTypeArguments.First();
                            if (proptypeGeneric.IsEnum ||
                                dicExecuteArrayRowReadClassOrTuple.ContainsKey(proptypeGeneric)) readExpAssign = Expression.New(RowInfo.Constructor,
                                    GetDataReaderValueBlockExpression(ctorParm.ParameterType, Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp)),
                                    //Expression.Call(MethodGetDataReaderValue, new Expression[] { Expression.Constant(ctorParm.ParameterType), Expression.Call(rowExp, MethodDataReaderGetValue, dataIndexExp) }),
                                    Expression.Add(dataIndexExp, Expression.Constant(1))
                            );
                            else
                            {
                                readExpAssign = Expression.New(RowInfo.Constructor,
                                    Expression.MakeMemberAccess(Expression.Call(MethodExecuteArrayRowReadClassOrTuple, new Expression[] { Expression.Constant(ctorParm.ParameterType), indexesExp, rowExp, dataIndexExp }), RowInfo.PropertyValue),
                                    Expression.Add(dataIndexExp, Expression.Constant(1)));
                            }
                        }
                        var varctorParm = Expression.Variable(ctorParm.ParameterType, $"ctorParm{ctorParm.Name}");
                        readExpValueParms.Add(varctorParm);
                        blockExp.AddRange(new Expression[] {
                            Expression.Assign(tryidxExp, dataIndexExp),
                            Expression.Assign(readExp, readExpAssign),
                            Expression.IfThen(Expression.GreaterThan(readExpDataIndex, dataIndexExp),
                                Expression.Assign(dataIndexExp, readExpDataIndex)),
                            Expression.IfThenElse(Expression.Equal(readExpValue, Expression.Constant(null)),
                                Expression.Assign(varctorParm, Expression.Default(ctorParm.ParameterType)),
                                Expression.Assign(varctorParm, Expression.Convert(readExpValue, ctorParm.ParameterType)))
                        });
                    }
                    blockExp.Add(Expression.Assign(retExp, Expression.New(ctor, readExpValueParms)));
                }
                else
                {
                    blockExp.AddRange(new Expression[] {
                        Expression.Assign(retExp, Expression.New(ctor)),
                        Expression.Assign(indexesLengthExp, Expression.Constant(0)),
                        Expression.IfThen(
                            Expression.NotEqual(indexesExp, Expression.Constant(null)),
                            Expression.Assign(indexesLengthExp, Expression.ArrayLength(indexesExp))
                        )
                    });

                    var props = type.GetProperties();//.ToDictionary(a => a.Name, a => a, StringComparer.CurrentCultureIgnoreCase);
                    var propIndex = 0;
                    foreach (var prop in props)
                    {
                        var propGetSetMethod = prop.GetSetMethod();
                        Expression readExpAssign = null; //加速缓存
                        if (prop.PropertyType.IsArray) readExpAssign = Expression.New(RowInfo.Constructor,
                            GetDataReaderValueBlockExpression(prop.PropertyType, Expression.Call(rowExp, MethodDataReaderGetValue, tryidxExp)),
                            //Expression.Call(MethodGetDataReaderValue, new Expression[] { Expression.Constant(prop.PropertyType), Expression.Call(rowExp, MethodDataReaderGetValue, tryidxExp) }),
                            Expression.Add(tryidxExp, Expression.Constant(1))
                        );
                        else
                        {
                            var proptypeGeneric = prop.PropertyType;
                            if (proptypeGeneric.IsNullableType()) proptypeGeneric = proptypeGeneric.GenericTypeArguments.First();
                            if (proptypeGeneric.IsEnum ||
                                dicExecuteArrayRowReadClassOrTuple.ContainsKey(proptypeGeneric)) readExpAssign = Expression.New(RowInfo.Constructor,
                                    GetDataReaderValueBlockExpression(prop.PropertyType, Expression.Call(rowExp, MethodDataReaderGetValue, tryidxExp)),
                                    //Expression.Call(MethodGetDataReaderValue, new Expression[] { Expression.Constant(prop.PropertyType), Expression.Call(rowExp, MethodDataReaderGetValue, tryidxExp) }),
                                    Expression.Add(tryidxExp, Expression.Constant(1))
                            );
                            else
                            {
                                ++propIndex;
                                continue;
                                //readExpAssign = Expression.Call(MethodExecuteArrayRowReadClassOrTuple, new Expression[] { Expression.Constant(prop.PropertyType), indexesExp, rowExp, tryidxExp });
                            }
                        }
                        blockExp.AddRange(new Expression[] {

                            Expression.IfThenElse(
                                Expression.LessThan(Expression.Constant(propIndex), indexesLengthExp),
                                Expression.Assign(tryidxExp, Expression.ArrayAccess(indexesExp, Expression.Constant(propIndex))),
                                Expression.Assign(tryidxExp, dataIndexExp)
                            ),
                            Expression.IfThen(
                                Expression.GreaterThanOrEqual(tryidxExp, Expression.Constant(0)),
                                Expression.Block(
                                    Expression.Assign(readExp, readExpAssign),
                                    Expression.IfThen(Expression.GreaterThan(readExpDataIndex, dataIndexExp),
                                        Expression.Assign(dataIndexExp, readExpDataIndex)),
                                    Expression.IfThenElse(
                                        Expression.Equal(readExpValue, Expression.Constant(null)),
                                        Expression.Call(retExp, propGetSetMethod, Expression.Default(prop.PropertyType)),
                                        Expression.Call(retExp, propGetSetMethod, Expression.Convert(readExpValue, prop.PropertyType)))
                                )
                            )
                        });
                        ++propIndex;
                    }
                }
                blockExp.AddRange(new Expression[] {
                    Expression.Return(returnTarget, Expression.New(RowInfo.Constructor, retExp, dataIndexExp)),
                    Expression.Label(returnTarget, Expression.Default(typeof(RowInfo)))
                });
                return Expression.Lambda<Func<Type, int[], DbDataReader, int, RowInfo>>(
                    Expression.Block(new[] { retExp, readExp, tryidxExp, readExpsIndex, indexesLengthExp }.Concat(readExpValueParms), blockExp), new[] { typeExp, indexesExp, rowExp, dataIndexExp }).Compile();
            });
            return func(type, indexes, row, dataIndex);
        }

        private static MethodInfo MethodExecuteArrayRowReadClassOrTuple = typeof(UtilsExpressionTree).GetMethod("ExecuteArrayRowReadClassOrTuple", BindingFlags.Static | BindingFlags.NonPublic);
        private static MethodInfo MethodGetDataReaderValue = typeof(UtilsExpressionTree).GetMethod("GetDataReaderValue", BindingFlags.Static | BindingFlags.NonPublic);

        private static ConcurrentDictionary<string, Action<object, object>> _dicFillPropertyValue = new ConcurrentDictionary<string, Action<object, object>>();
        private static void FillPropertyValue(object info, string memberAccessPath, object value)
        {
            var typeObj = info.GetType();
            var typeValue = value.GetType();
            var key = "FillPropertyValue_" + typeObj.FullName + "_" + typeValue.FullName;
            var act = _dicFillPropertyValue.GetOrAdd($"{key}.{memberAccessPath}", s =>
            {
                var parmInfo = Expression.Parameter(typeof(object), "info");
                var parmValue = Expression.Parameter(typeof(object), "value");
                Expression exp = Expression.Convert(parmInfo, typeObj);
                foreach (var pro in memberAccessPath.Split('.'))
                    exp = Expression.PropertyOrField(exp, pro) ?? throw new Exception(string.Concat(exp.Type.FullName, " 没有定义属性 ", pro));

                var value2 = Expression.Call(MethodGetDataReaderValue, Expression.Constant(exp.Type), parmValue);
                var value3 = Expression.Convert(parmValue, typeValue);
                exp = Expression.Assign(exp, value3);
                return Expression.Lambda<Action<object, object>>(exp, parmInfo, parmValue).Compile();
            });
            act(info, value);
        }

        private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, Func<object, object>>> _dicGetDataReaderValue = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, Func<object, object>>>();
        private static MethodInfo MethodArrayGetValue = typeof(Array).GetMethod("GetValue", new[] { typeof(int) });
        private static MethodInfo MethodArrayGetLength = typeof(Array).GetMethod("GetLength", new[] { typeof(int) });
        //static MethodInfo MethodMygisGeometryParse = typeof(MygisGeometry).GetMethod("Parse", new[] { typeof(string) });
        private static MethodInfo MethodGuidParse = typeof(Guid).GetMethod("Parse", new[] { typeof(string) });
        private static MethodInfo MethodEnumParse = typeof(Enum).GetMethod("Parse", new[] { typeof(Type), typeof(string), typeof(bool) });
        private static MethodInfo MethodToString = typeof(string).GetMethod("Concat", new[] { typeof(object) });
        private static MethodInfo MethodConvertChangeType = typeof(Convert).GetMethod("ChangeType", new[] { typeof(object), typeof(Type) });
        private static MethodInfo MethodTimeSpanFromSeconds = typeof(TimeSpan).GetMethod("FromSeconds");
        private static MethodInfo MethodDoubleParse = typeof(double).GetMethod("Parse", new[] { typeof(string) });
        private static MethodInfo MethodJTokenParse = typeof(JToken).GetMethod("Parse", new[] { typeof(string) });
        private static MethodInfo MethodJObjectParse = typeof(JObject).GetMethod("Parse", new[] { typeof(string) });
        private static MethodInfo MethodJArrayParse = typeof(JArray).GetMethod("Parse", new[] { typeof(string) });
        private static Expression GetDataReaderValueBlockExpression(Type type, Expression value)
        {
            var returnTarget = Expression.Label(typeof(object));
            var valueExp = Expression.Variable(typeof(object), "locvalue");
            Func<Expression> funcGetExpression = () =>
            {
                if (type.FullName == "System.Byte[]") return Expression.Return(returnTarget, valueExp);
                if (type.IsArray)
                {
                    var elementType = type.GetElementType();
                    var arrNewExp = Expression.Variable(type, "arrNew");
                    var arrExp = Expression.Variable(typeof(Array), "arr");
                    var arrLenExp = Expression.Variable(typeof(int), "arrLen");
                    var arrXExp = Expression.Variable(typeof(int), "arrX");
                    var arrReadValExp = Expression.Variable(typeof(object), "arrReadVal");
                    var label = Expression.Label(typeof(int));
                    return Expression.IfThenElse(
                        Expression.TypeEqual(valueExp, type),
                        Expression.Return(returnTarget, valueExp),
                        Expression.Block(
                            new[] { arrNewExp, arrExp, arrLenExp, arrXExp, arrReadValExp },
                            Expression.Assign(arrExp, Expression.TypeAs(valueExp, typeof(Array))),
                            Expression.Assign(arrLenExp, Expression.Call(arrExp, MethodArrayGetLength, Expression.Constant(0))),
                            Expression.Assign(arrXExp, Expression.Constant(0)),
                            Expression.Assign(arrNewExp, Expression.NewArrayBounds(elementType, arrLenExp)),
                            Expression.Loop(
                                Expression.IfThenElse(
                                    Expression.LessThan(arrXExp, arrLenExp),
                                    Expression.Block(
                                        Expression.Assign(arrReadValExp, GetDataReaderValueBlockExpression(elementType, Expression.Call(arrExp, MethodArrayGetValue, arrXExp))),
                                        Expression.IfThenElse(
                                            Expression.Equal(arrReadValExp, Expression.Constant(null)),
                                            Expression.Assign(Expression.ArrayAccess(arrNewExp, arrXExp), Expression.Default(elementType)),
                                            Expression.Assign(Expression.ArrayAccess(arrNewExp, arrXExp), Expression.Convert(arrReadValExp, elementType))
                                        ),
                                        Expression.PostIncrementAssign(arrXExp)
                                    ),
                                    Expression.Break(label, arrXExp)
                                ),
                                label
                            ),
                            Expression.Return(returnTarget, arrNewExp)
                        )
                    );
                }
                if (type.IsNullableType())
                    type = type.GenericTypeArguments.First();
                if (type.IsEnum) return Expression.Return(returnTarget, Expression.Call(MethodEnumParse, Expression.Constant(type, typeof(Type)), Expression.Call(MethodToString, valueExp), Expression.Constant(true, typeof(bool))));
                switch (type.FullName)
                {
                    case "System.Guid":
                        return Expression.IfThenElse(
            Expression.TypeEqual(valueExp, type),
            Expression.Return(returnTarget, valueExp),
            Expression.Return(returnTarget, Expression.Convert(Expression.Call(MethodGuidParse, Expression.Convert(valueExp, typeof(string))), typeof(object)))
        );
                    //case "MygisPoint": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodMygisGeometryParse, Expression.Convert(valueExp, typeof(string))), typeof(MygisPoint)));
                    //case "MygisLineString": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodMygisGeometryParse, Expression.Convert(valueExp, typeof(string))), typeof(MygisLineString)));
                    //case "MygisPolygon": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodMygisGeometryParse, Expression.Convert(valueExp, typeof(string))), typeof(MygisPolygon)));
                    //case "MygisMultiPoint": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodMygisGeometryParse, Expression.Convert(valueExp, typeof(string))), typeof(MygisMultiPoint)));
                    //case "MygisMultiLineString": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodMygisGeometryParse, Expression.Convert(valueExp, typeof(string))), typeof(MygisMultiLineString)));
                    //case "MygisMultiPolygon": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodMygisGeometryParse, Expression.Convert(valueExp, typeof(string))), typeof(MygisMultiPolygon)));
                    case "Newtonsoft.Json.Linq.JToken": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodJTokenParse, Expression.Convert(valueExp, typeof(string))), typeof(JToken)));
                    case "Newtonsoft.Json.Linq.JObject": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodJObjectParse, Expression.Convert(valueExp, typeof(string))), typeof(JObject)));
                    case "Newtonsoft.Json.Linq.JArray": return Expression.Return(returnTarget, Expression.TypeAs(Expression.Call(MethodJArrayParse, Expression.Convert(valueExp, typeof(string))), typeof(JArray)));
                    case "Npgsql.LegacyPostgis.PostgisGeometry": return Expression.Return(returnTarget, valueExp);
                    case "System.TimeSpan":
                        return Expression.IfThenElse(
        Expression.TypeEqual(valueExp, type),
        Expression.Return(returnTarget, valueExp),
        Expression.Return(returnTarget, Expression.Convert(Expression.Call(MethodTimeSpanFromSeconds, Expression.Call(MethodDoubleParse, Expression.Call(MethodToString, valueExp))), typeof(object)))
    );
                }
                return Expression.IfThenElse(
                    Expression.TypeEqual(valueExp, type),
                    Expression.Return(returnTarget, valueExp),
                    Expression.Return(returnTarget, Expression.Call(MethodConvertChangeType, valueExp, Expression.Constant(type, typeof(Type))))
                );
            };

            return Expression.Block(
                new[] { valueExp },
                Expression.Assign(valueExp, value),
                Expression.IfThenElse(
                    Expression.Or(
                        Expression.Equal(valueExp, Expression.Constant(null)),
                        Expression.Equal(valueExp, Expression.Constant(DBNull.Value))
                    ),
                    Expression.Return(returnTarget, Expression.Convert(Expression.Default(type), typeof(object))),
                    funcGetExpression()
                ),
                Expression.Label(returnTarget, Expression.Default(typeof(object)))
            );
        }
        private static object GetDataReaderValue(Type type, object value)
        {
            if (value == null || value == DBNull.Value) return null;
            var func = _dicGetDataReaderValue.GetOrAdd(type, k1 => new ConcurrentDictionary<Type, Func<object, object>>()).GetOrAdd(value.GetType(), valueType =>
            {
                var parmExp = Expression.Parameter(typeof(object), "value");
                var exp = GetDataReaderValueBlockExpression(type, parmExp);
                return Expression.Lambda<Func<object, object>>(exp, parmExp).Compile();
            });
            return func(value);

        }

        private static string GetCsName(string name)
        {
            name = Regex.Replace(name.TrimStart('@'), @"[^\w]", "_");
            return char.IsLetter(name, 0) ? name : string.Concat("_", name);
        }
    }
}

