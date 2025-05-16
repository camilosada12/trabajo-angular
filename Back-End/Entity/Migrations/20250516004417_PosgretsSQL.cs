using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class PosgretsSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phonenumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FormModules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    formid = table.Column<int>(type: "integer", nullable: false),
                    moduleid = table.Column<int>(type: "integer", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormModules", x => x.id);
                    table.ForeignKey(
                        name: "FK_FormModule_Form",
                        column: x => x.formid,
                        principalTable: "Forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_FormModules",
                        column: x => x.moduleid,
                        principalTable: "Modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: true),
                    personid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Person_User",
                        column: x => x.personid,
                        principalTable: "Persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolFormPermissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    rolid = table.Column<int>(type: "integer", nullable: false),
                    formid = table.Column<int>(type: "integer", nullable: false),
                    permissionid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolFormPermissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Form",
                        column: x => x.formid,
                        principalTable: "Forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Permission",
                        column: x => x.permissionid,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Rol",
                        column: x => x.rolid,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolUsers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolUser_Rol",
                        column: x => x.rolid,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolUser_User",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Forms",
                columns: new[] { "id", "active", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, true, "Formulario principal del sistema", false, "Formulario Principal" },
                    { 2, true, "Formulario secundario", false, "Formulario Secundario" }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "id", "active", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, true, "Módulo para administración general", false, "Módulo Administrativo" },
                    { 2, true, "Módulo encargado de generar reportes", false, "Módulo de Reportes" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "id", "active", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, true, "Permiso para lectura", false, "Leer" },
                    { 2, true, "Permiso para escritura", false, "Escribir" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "id", "active", "firstname", "isdeleted", "lastname", "phonenumber" },
                values: new object[,]
                {
                    { 1, true, "Juan", false, "Pérez", "1234567890" },
                    { 2, true, "Sara", false, "sofia", "312312314" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, "Rol con todos los permisos", false, "Administrador" },
                    { 2, "Rol estándar para usuarios normales", false, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "FormModules",
                columns: new[] { "id", "formid", "isdeleted", "moduleid" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 1 }
                });

            migrationBuilder.InsertData(
                table: "RolFormPermissions",
                columns: new[] { "id", "formid", "isdeleted", "permissionid", "rolid" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 1 },
                    { 2, 1, false, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "active", "email", "isdeleted", "password", "personid", "username" },
                values: new object[,]
                {
                    { 1, true, "camilo@gmail.com", false, "admin123", 1, "camilosada12" },
                    { 2, true, "sarita@gmail.com", false, "sara12312", 2, "sara12312" }
                });

            migrationBuilder.InsertData(
                table: "RolUsers",
                columns: new[] { "id", "isdeleted", "rolid", "userid" },
                values: new object[,]
                {
                    { 1, false, 1, 1 },
                    { 2, false, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormModules_formid",
                table: "FormModules",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_FormModules_moduleid",
                table: "FormModules",
                column: "moduleid");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermissions_formid",
                table: "RolFormPermissions",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermissions_permissionid",
                table: "RolFormPermissions",
                column: "permissionid");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermissions_rolid",
                table: "RolFormPermissions",
                column: "rolid");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsers_rolid",
                table: "RolUsers",
                column: "rolid");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsers_userid",
                table: "RolUsers",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_personid",
                table: "Users",
                column: "personid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormModules");

            migrationBuilder.DropTable(
                name: "RolFormPermissions");

            migrationBuilder.DropTable(
                name: "RolUsers");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
