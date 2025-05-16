using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class PostgreSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "form",
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
                    table.PrimaryKey("PK_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "module",
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
                    table.PrimaryKey("PK_module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
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
                    table.PrimaryKey("PK_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person",
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
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
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
                    table.PrimaryKey("PK_rol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "formmodule",
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
                    table.PrimaryKey("PK_formmodule", x => x.id);
                    table.ForeignKey(
                        name: "FK_FormModule_Form",
                        column: x => x.formid,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_FormModules",
                        column: x => x.moduleid,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
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
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_Person_User",
                        column: x => x.personid,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rolformpermission",
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
                    table.PrimaryKey("PK_rolformpermission", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Form",
                        column: x => x.formid,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Permission",
                        column: x => x.permissionid,
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Rol",
                        column: x => x.rolid,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roluser",
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
                    table.PrimaryKey("PK_roluser", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolUser_Rol",
                        column: x => x.rolid,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolUser_User",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "form",
                columns: new[] { "id", "active", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, true, "Formulario principal del sistema", false, "Formulario Principal" },
                    { 2, true, "Formulario secundario", false, "Formulario Secundario" }
                });

            migrationBuilder.InsertData(
                table: "module",
                columns: new[] { "id", "active", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, true, "Módulo para administración general", false, "Módulo Administrativo" },
                    { 2, true, "Módulo encargado de generar reportes", false, "Módulo de Reportes" }
                });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "active", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, true, "Permiso para lectura", false, "Leer" },
                    { 2, true, "Permiso para escritura", false, "Escribir" }
                });

            migrationBuilder.InsertData(
                table: "person",
                columns: new[] { "id", "active", "firstname", "isdeleted", "lastname", "phonenumber" },
                values: new object[,]
                {
                    { 1, true, "Juan", false, "Pérez", "1234567890" },
                    { 2, true, "Sara", false, "sofia", "312312314" }
                });

            migrationBuilder.InsertData(
                table: "rol",
                columns: new[] { "id", "description", "isdeleted", "name" },
                values: new object[,]
                {
                    { 1, "Rol con todos los permisos", false, "Administrador" },
                    { 2, "Rol estándar para usuarios normales", false, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "formmodule",
                columns: new[] { "id", "formid", "isdeleted", "moduleid" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 1 }
                });

            migrationBuilder.InsertData(
                table: "rolformpermission",
                columns: new[] { "id", "formid", "isdeleted", "permissionid", "rolid" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 1 },
                    { 2, 1, false, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "active", "email", "isdeleted", "password", "personid", "username" },
                values: new object[,]
                {
                    { 1, true, "camilo@gmail.com", false, "admin123", 1, "camilosada12" },
                    { 2, true, "sarita@gmail.com", false, "sara12312", 2, "sara12312" }
                });

            migrationBuilder.InsertData(
                table: "roluser",
                columns: new[] { "id", "isdeleted", "rolid", "userid" },
                values: new object[,]
                {
                    { 1, false, 1, 1 },
                    { 2, false, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_formmodule_formid",
                table: "formmodule",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_formmodule_moduleid",
                table: "formmodule",
                column: "moduleid");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_formid",
                table: "rolformpermission",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_permissionid",
                table: "rolformpermission",
                column: "permissionid");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_rolid",
                table: "rolformpermission",
                column: "rolid");

            migrationBuilder.CreateIndex(
                name: "IX_roluser_rolid",
                table: "roluser",
                column: "rolid");

            migrationBuilder.CreateIndex(
                name: "IX_roluser_userid",
                table: "roluser",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_user_personid",
                table: "user",
                column: "personid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formmodule");

            migrationBuilder.DropTable(
                name: "rolformpermission");

            migrationBuilder.DropTable(
                name: "roluser");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "form");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
