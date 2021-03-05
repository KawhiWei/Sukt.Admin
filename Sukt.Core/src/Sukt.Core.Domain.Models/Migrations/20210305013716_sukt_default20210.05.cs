using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.Domain.Models.Migrations
{
    public partial class sukt_default2021005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false, defaultValue: true),
                    AllowedAccessTokenSigningAlgorithms = table.Column<string>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiScope",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false, defaultValue: true),
                    Emphasize = table.Column<bool>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    ClientId = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    ProtocolType = table.Column<string>(nullable: true),
                    RequireClientSecret = table.Column<bool>(nullable: false, defaultValue: true),
                    Description = table.Column<string>(nullable: true),
                    ClientUri = table.Column<string>(nullable: true),
                    LogoUri = table.Column<string>(nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false, defaultValue: true),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(nullable: false),
                    RequirePkce = table.Column<bool>(nullable: false, defaultValue: true),
                    AllowPlainTextPkce = table.Column<bool>(nullable: false),
                    RequireRequestObject = table.Column<bool>(nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(nullable: false, defaultValue: true),
                    BackChannelLogoutUri = table.Column<string>(nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(nullable: false, defaultValue: true),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false, defaultValue: 300),
                    AllowedIdentityTokenSigningAlgorithms = table.Column<string>(nullable: true),
                    AccessTokenLifetime = table.Column<int>(nullable: false, defaultValue: 3600),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false, defaultValue: 300),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false, defaultValue: 2592000),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false, defaultValue: 2592000),
                    RefreshTokenUsage = table.Column<int>(nullable: false, defaultValue: -1),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false, defaultValue: -1),
                    AccessTokenType = table.Column<int>(nullable: false),
                    EnableLocalLogin = table.Column<bool>(nullable: false, defaultValue: true),
                    IncludeJwtId = table.Column<bool>(nullable: false, defaultValue: true),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    ClientClaimsPrefix = table.Column<string>(nullable: true, defaultValue: "client_"),
                    PairWiseSubjectSalt = table.Column<string>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(nullable: true),
                    DeviceCodeLifetime = table.Column<int>(nullable: false, defaultValue: 300),
                    NonEditable = table.Column<bool>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataDictionary",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: false, defaultValue: 0),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFlowCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeviceCode = table.Column<string>(nullable: true),
                    UserCode = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true),
                    SessionId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: true),
                    ConsumedTime = table.Column<DateTime>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceFlowCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Function",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    LinkUrl = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    NonEditable = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(maxLength: 200, nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    Icon = table.Column<string>(maxLength: 200, nullable: false),
                    ParentNumber = table.Column<string>(nullable: true),
                    Component = table.Column<string>(maxLength: 200, nullable: false),
                    ComponentName = table.Column<string>(maxLength: 200, nullable: false),
                    IsShow = table.Column<bool>(nullable: false, defaultValue: true),
                    Sort = table.Column<int>(nullable: false, defaultValue: 0),
                    ButtonClick = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    MicroName = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuFunction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false),
                    FunctionId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFunction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    LinkMan = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiTenants", x => x.Id);
                },
                comment: "租户信息表");

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ParentNumber = table.Column<string>(nullable: true),
                    Depth = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FirstLeader = table.Column<Guid>(nullable: true),
                    SecondLeader = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    OrganizationNumber = table.Column<Guid>(nullable: false),
                    PositionId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true),
                    SessionId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: true),
                    ConsumedTime = table.Column<DateTime>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(maxLength: 500, nullable: true),
                    ClaimValue = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 50, nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizeEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    HeadImg = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false, defaultValue: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(nullable: false, defaultValue: 0),
                    IsSystem = table.Column<bool>(nullable: false),
                    Sex = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Education = table.Column<string>(nullable: true),
                    TechnicalLevel = table.Column<string>(nullable: true),
                    IdCard = table.Column<string>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    Duties = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: false),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 450, nullable: true),
                    Name = table.Column<string>(maxLength: 450, nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ApiResourceId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceClaim_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ApiResourceId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceProperty_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceScope",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Scope = table.Column<string>(nullable: true),
                    ApiResourceId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceScope_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceSecret",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    ApiResourceId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResourceSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResourceSecret_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeClaim_ApiScope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "ApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ScopeId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeProperty_ApiScope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "ApiScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientClaim_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientCorsOrigin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Origin = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCorsOrigin_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientGrantType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GrantType = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientGrantType_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdPRestriction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Provider = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdPRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientIdPRestriction_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientPostLogoutRedirectUri",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostLogoutRedirectUri = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPostLogoutRedirectUri_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProperty_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientRedirectUri",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RedirectUri = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRedirectUri_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientScope",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Scope = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientScope_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecret",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSecret_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdentityResourceId = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceClaim_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    IdentityResourceId = table.Column<Guid>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityResourceProperty_IdentityResource_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceClaim_ApiResourceId",
                table: "ApiResourceClaim",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceProperty_ApiResourceId",
                table: "ApiResourceProperty",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceScope_ApiResourceId",
                table: "ApiResourceScope",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceSecret_ApiResourceId",
                table: "ApiResourceSecret",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeClaim_ScopeId",
                table: "ApiScopeClaim",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeProperty_ScopeId",
                table: "ApiScopeProperty",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientClaim_ClientId",
                table: "ClientClaim",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCorsOrigin_ClientId",
                table: "ClientCorsOrigin",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGrantType_ClientId",
                table: "ClientGrantType",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdPRestriction_ClientId",
                table: "ClientIdPRestriction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPostLogoutRedirectUri_ClientId",
                table: "ClientPostLogoutRedirectUri",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProperty_ClientId",
                table: "ClientProperty",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRedirectUri_ClientId",
                table: "ClientRedirectUri",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientScope_ClientId",
                table: "ClientScope",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecret_ClientId",
                table: "ClientSecret",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceClaim_IdentityResourceId",
                table: "IdentityResourceClaim",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResourceProperty_IdentityResourceId",
                table: "IdentityResourceProperty",
                column: "IdentityResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiResourceClaim");

            migrationBuilder.DropTable(
                name: "ApiResourceProperty");

            migrationBuilder.DropTable(
                name: "ApiResourceScope");

            migrationBuilder.DropTable(
                name: "ApiResourceSecret");

            migrationBuilder.DropTable(
                name: "ApiScopeClaim");

            migrationBuilder.DropTable(
                name: "ApiScopeProperty");

            migrationBuilder.DropTable(
                name: "ClientClaim");

            migrationBuilder.DropTable(
                name: "ClientCorsOrigin");

            migrationBuilder.DropTable(
                name: "ClientGrantType");

            migrationBuilder.DropTable(
                name: "ClientIdPRestriction");

            migrationBuilder.DropTable(
                name: "ClientPostLogoutRedirectUri");

            migrationBuilder.DropTable(
                name: "ClientProperty");

            migrationBuilder.DropTable(
                name: "ClientRedirectUri");

            migrationBuilder.DropTable(
                name: "ClientScope");

            migrationBuilder.DropTable(
                name: "ClientSecret");

            migrationBuilder.DropTable(
                name: "DataDictionary");

            migrationBuilder.DropTable(
                name: "DeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "Function");

            migrationBuilder.DropTable(
                name: "IdentityResourceClaim");

            migrationBuilder.DropTable(
                name: "IdentityResourceProperty");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuFunction");

            migrationBuilder.DropTable(
                name: "MultiTenants");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "OrganizationUser");

            migrationBuilder.DropTable(
                name: "PersistedGrant");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "RoleMenu");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "ApiResource");

            migrationBuilder.DropTable(
                name: "ApiScope");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "IdentityResource");
        }
    }
}
