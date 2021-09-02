# 菜单管理后台开发文档：

## API路由地址：api/Menu/

### 添加菜单：CreateAsync

### 修改菜单：UpdateAsync

| 字段名称      | 类型   | 说明                   | 其他                                                         |
| ------------- | ------ | ---------------------- | ------------------------------------------------------------ |
| Id            | Guid   | 菜单唯一主键           |                                                              |
| Name          | string | 菜单名称               |                                                              |
| Path          | string | 路由地址               |                                                              |
| ParentId      | Guid   | 父级ID                 |                                                              |
| Icon          | string | 菜单图标               |                                                              |
| ParentNumber  | string | 当前菜单以上所有的父级 |                                                              |
| Component     | string | 组件地址               |                                                              |
| ComponentName | string | 组件名称               |                                                              |
| IsShow        | bool   | 是否显示               |                                                              |
| Sort          | int    | 排序                   |                                                              |
| ButtonClick   | string | 按钮事件               |                                                              |
| Type          | Enum   | 菜单类型               | [Description("菜单")]<br/>MenuType = 0,<br/>[Description("Tab页")]<br/>Tab = 5,<br/>[Description("按钮")]<br/>Button = 10, |
| MicroName     | string | 菜单对应子应用         |                                                              |

### 根据Id获取一个菜单：GetLoadFromMenuAsync

| 字段名称      | 类型       | 说明                   | 其他                                                         |
| ------------- | ---------- | ---------------------- | ------------------------------------------------------------ |
| Id            | Guid       | 菜单唯一主键           |                                                              |
| Name          | string     | 菜单名称               |                                                              |
| Path          | string     | 路由地址               |                                                              |
| ParentId      | Guid       | 父级ID                 |                                                              |
| Icon          | string     | 菜单图标               |                                                              |
| ParentNumber  | string     | 当前菜单以上所有的父级 |                                                              |
| Component     | string     | 组件地址               |                                                              |
| ComponentName | string     | 组件名称               |                                                              |
| IsShow        | bool       | 是否显示               |                                                              |
| Sort          | int        | 排序                   |                                                              |
| ButtonClick   | string     | 按钮事件               |                                                              |
| Type          | Enum       | 菜单类型               | [Description("菜单")] <br/>MenuType = 0, <br/>[Description("Tab页")]<br/> Tab = 5,<br/> [Description("按钮")] <br/>Button = 10, |
| MicroName     | string     | 菜单对应子应用         |                                                              |
| FuncIds       | List<Guid> | 菜单对应功能的Id       |                                                              |

### 根据当前用户用户路由菜单：GetUserMenuTreeAsync

| 字段名称      | 类型           | 说明                   | 其他                                                         |
| ------------- | -------------- | ---------------------- | ------------------------------------------------------------ |
| Id            | Guid           | 菜单唯一主键           |                                                              |
| Name          | string         | 菜单名称               |                                                              |
| Path          | string         | 路由地址               |                                                              |
| ParentId      | Guid           | 父级ID                 |                                                              |
| Icon          | string         | 菜单图标               |                                                              |
| ParentNumber  | string         | 当前菜单以上所有的父级 |                                                              |
| Component     | string         | 组件地址               |                                                              |
| ComponentName | string         | 组件名称               |                                                              |
| IsShow        | bool           | 是否显示               |                                                              |
| Sort          | int            | 排序                   |                                                              |
| ButtonClick   | string         | 按钮事件               |                                                              |
| Type          | Enum           | 菜单类型               | [Description("菜单")]  MenuType = 0,  [Description("Tab页")] Tab = 5, [Description("按钮")]  Button = 10, |
| MicroName     | string         | 菜单对应子应用         |                                                              |
| Children      | List<当前对象> | 菜单子级               |                                                              |
| Tabs          | List<当前对象> | 菜单tab页              |                                                              |
| Buttons       | List<当前对象> | 菜单拥有的按钮         |                                                              |