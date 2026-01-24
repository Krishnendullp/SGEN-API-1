using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SGen.Framework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ladger_category",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    category_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort_order = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ladger_category", x => new { x.tenant_id, x.company_id, x.id });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "project_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    loa_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_master", x => new { x.tenant_id, x.company_id, x.project_id });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tax_group",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_group", x => new { x.tenant_id, x.company_id, x.tax_group_id });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    unit_id = table.Column<int>(type: "int", nullable: false),
                    unit_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    conversion_value = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit", x => new { x.tenant_id, x.company_id, x.unit_id });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "voucher_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_no = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    voucher_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    voucher_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    reference_no = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    narration = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_posted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    is_cancelled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher_master", x => new { x.tenant_id, x.company_id, x.voucher_id });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ledger_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    ledger_cate_id = table.Column<int>(type: "int", nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    ledger_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_control_ledger = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    opening_balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    opening_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    debit_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    credit_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    gst_no = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ledger_master", x => new { x.tenant_id, x.company_id, x.ledger_id });
                    table.ForeignKey(
                        name: "fk_ledger_master_ladger_category",
                        columns: x => new { x.tenant_id, x.company_id, x.ledger_cate_id },
                        principalTable: "ladger_category",
                        principalColumns: new[] { "tenant_id", "company_id", "id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_item_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_item_master", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_sales_item_tax_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "item_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    unit_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    tax_group_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hsn_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rate = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    gst_percent = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_master", x => new { x.tenant_id, x.company_id, x.item_id });
                    table.ForeignKey(
                        name: "FK_item_master_tax_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" });
                    table.ForeignKey(
                        name: "FK_item_master_unit",
                        columns: x => new { x.tenant_id, x.company_id, x.unit_id },
                        principalTable: "unit",
                        principalColumns: new[] { "tenant_id", "company_id", "unit_id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    ledger_Id = table.Column<int>(type: "int", nullable: true),
                    customer_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_person = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pincode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gst_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "admin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_master", x => new { x.tenant_id, x.company_id, x.customer_id });
                    table.ForeignKey(
                        name: "fk_customer_ledger",
                        columns: x => new { x.tenant_id, x.company_id, x.ledger_Id },
                        principalTable: "ledger_master",
                        principalColumns: new[] { "tenant_id", "company_id", "ledger_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sub_ledger",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    sub_ledger_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: true),
                    sub_ledger_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gst_no = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_ledger", x => new { x.tenant_id, x.company_id, x.sub_ledger_id });
                    table.ForeignKey(
                        name: "fk_sub_ledger_ledger_master",
                        columns: x => new { x.tenant_id, x.company_id, x.ledger_id },
                        principalTable: "ledger_master",
                        principalColumns: new[] { "tenant_id", "company_id", "ledger_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: true),
                    supplier_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    supplier_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_person = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pincode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gst_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "admin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier_master", x => new { x.tenant_id, x.company_id, x.supplier_id });
                    table.ForeignKey(
                        name: "fk_supplier_ledger",
                        columns: x => new { x.tenant_id, x.company_id, x.ledger_id },
                        principalTable: "ledger_master",
                        principalColumns: new[] { "tenant_id", "company_id", "ledger_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tax",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tax_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taxs", x => new { x.tenant_id, x.company_id, x.tax_id });
                    table.ForeignKey(
                        name: "fk_tax_ledger_composite",
                        columns: x => new { x.tenant_id, x.company_id, x.ledger_id },
                        principalTable: "ledger_master",
                        principalColumns: new[] { "tenant_id", "company_id", "ledger_id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    taxable_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    total_tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    discount_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    sale_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    inv_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fin_yr = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payment_receive_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0.00m),
                    discount = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_note = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    item_tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    net_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_master", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_sale_customer",
                        columns: x => new { x.tenant_id, x.company_id, x.customer_id },
                        principalTable: "customer_master",
                        principalColumns: new[] { "tenant_id", "company_id", "customer_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_master_project",
                        columns: x => new { x.tenant_id, x.company_id, x.project_id },
                        principalTable: "project_master",
                        principalColumns: new[] { "tenant_id", "company_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_master_tax_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ledger_detail",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: true),
                    sub_ledger_id = table.Column<int>(type: "int", nullable: true),
                    entry_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ref_id = table.Column<long>(type: "bigint", nullable: true),
                    account_type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    debit_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    credit_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    entry_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    narration = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ledger_detail", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_ledger_details_ledger",
                        columns: x => new { x.tenant_id, x.company_id, x.ledger_id },
                        principalTable: "ledger_master",
                        principalColumns: new[] { "tenant_id", "company_id", "ledger_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ledger_details_subledger",
                        columns: x => new { x.tenant_id, x.company_id, x.sub_ledger_id },
                        principalTable: "sub_ledger",
                        principalColumns: new[] { "tenant_id", "company_id", "sub_ledger_id" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "purchase_order",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    po_id = table.Column<int>(type: "int", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    po_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    po_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    total_taxable_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    total_discount = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    total_tax = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    net_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    round_off = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remarks = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    fin_year = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    due_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order", x => new { x.tenant_id, x.company_id, x.po_id });
                    table.ForeignKey(
                        name: "FK_po_order_project",
                        columns: x => new { x.tenant_id, x.company_id, x.project_id },
                        principalTable: "project_master",
                        principalColumns: new[] { "tenant_id", "company_id", "project_id" });
                    table.ForeignKey(
                        name: "FK_purchase_order_supplier",
                        columns: x => new { x.tenant_id, x.company_id, x.supplier_id },
                        principalTable: "supplier_master",
                        principalColumns: new[] { "tenant_id", "company_id", "supplier_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_return",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    return_id = table.Column<int>(type: "int", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    return_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    return_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    net_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    round_off = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Returned")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remarks = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_return", x => new { x.tenant_id, x.company_id, x.return_id });
                    table.ForeignKey(
                        name: "fk_supplier_return_project",
                        columns: x => new { x.tenant_id, x.company_id, x.project_id },
                        principalTable: "project_master",
                        principalColumns: new[] { "tenant_id", "company_id", "project_id" });
                    table.ForeignKey(
                        name: "fk_supplier_return_supplier",
                        columns: x => new { x.tenant_id, x.company_id, x.supplier_id },
                        principalTable: "supplier_master",
                        principalColumns: new[] { "tenant_id", "company_id", "supplier_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tax_group_igst_detail",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    tax_id = table.Column<int>(type: "int", nullable: true),
                    serial_no = table.Column<int>(type: "int", nullable: true),
                    rate = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_group_igst_detail", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "FK_tax_group_igst_detail_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" });
                    table.ForeignKey(
                        name: "FK_tax_group_igst_tax",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_id },
                        principalTable: "tax",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tax_group_sgst_detail",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    tax_id = table.Column<int>(type: "int", nullable: true),
                    serial_no = table.Column<int>(type: "int", nullable: true),
                    rate = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_group_sgst_detail", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "FK_tax_group_sgst_detail_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" });
                    table.ForeignKey(
                        name: "FK_tax_group_sgst_tax",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_id },
                        principalTable: "tax",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_invoice_payment",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    invoice_payment_id = table.Column<long>(type: "bigint", nullable: false),
                    remarks = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pay_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    pay_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    payment_mode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_active = table.Column<ulong>(type: "bit", nullable: false, defaultValue: 1ul)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_invoice_payment", x => new { x.tenant_id, x.company_id, x.invoice_payment_id });
                    table.ForeignKey(
                        name: "fk_sale_master_invoice_payment",
                        columns: x => new { x.tenant_id, x.company_id, x.sale_id },
                        principalTable: "sale_master",
                        principalColumns: new[] { "tenant_id", "company_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_master_detail",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1.00m),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    taxable_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    total_tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_master_detail", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_sale_detail_tax_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_details_item",
                        columns: x => new { x.tenant_id, x.company_id, x.item_id },
                        principalTable: "sale_item_master",
                        principalColumns: new[] { "tenant_id", "company_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_details_sale",
                        columns: x => new { x.tenant_id, x.company_id, x.sale_id },
                        principalTable: "sale_master",
                        principalColumns: new[] { "tenant_id", "company_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_master_tax",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    tax_percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_master_tax", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_sale_master_tax_sale",
                        columns: x => new { x.tenant_id, x.company_id, x.sale_id },
                        principalTable: "sale_master",
                        principalColumns: new[] { "tenant_id", "company_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "store_transaction",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    po_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    tax_group_id = table.Column<int>(type: "int", nullable: false),
                    transaction_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    net_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    remarks = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "admin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    total_taxable_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    total_tax = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    unit_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_transaction", x => new { x.tenant_id, x.company_id, x.store_id });
                    table.ForeignKey(
                        name: "FK_store_transaction_item_master",
                        columns: x => new { x.tenant_id, x.company_id, x.item_id },
                        principalTable: "item_master",
                        principalColumns: new[] { "tenant_id", "company_id", "item_id" });
                    table.ForeignKey(
                        name: "FK_store_transaction_purchase_order",
                        columns: x => new { x.tenant_id, x.company_id, x.po_id },
                        principalTable: "purchase_order",
                        principalColumns: new[] { "tenant_id", "company_id", "po_id" });
                    table.ForeignKey(
                        name: "FK_store_transaction_tax_group",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_group_id },
                        principalTable: "tax_group",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_group_id" });
                    table.ForeignKey(
                        name: "FK_store_transaction_unit",
                        columns: x => new { x.tenant_id, x.company_id, x.unit_id },
                        principalTable: "unit",
                        principalColumns: new[] { "tenant_id", "company_id", "unit_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_payment",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    payment_id = table.Column<int>(type: "int", nullable: false),
                    po_id = table.Column<int>(type: "int", nullable: true),
                    supplier_id = table.Column<int>(type: "int", nullable: true),
                    payment_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    cash = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    card = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    cheque = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    upi = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    neft = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    pay_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    deu_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    payment_mode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reference_no = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remarks = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier_payment", x => new { x.tenant_id, x.company_id, x.payment_id });
                    table.ForeignKey(
                        name: "FK_supplier_payment_purchase_order",
                        columns: x => new { x.tenant_id, x.company_id, x.po_id },
                        principalTable: "purchase_order",
                        principalColumns: new[] { "tenant_id", "company_id", "po_id" });
                    table.ForeignKey(
                        name: "FK_supplier_payment_supplier_master",
                        columns: x => new { x.tenant_id, x.company_id, x.supplier_id },
                        principalTable: "supplier_master",
                        principalColumns: new[] { "tenant_id", "company_id", "supplier_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier_return_detail",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    return_detail_id = table.Column<int>(type: "int", nullable: false),
                    return_id = table.Column<int>(type: "int", nullable: false),
                    po_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true, computedColumnSql: "`qty` * `price`", stored: true),
                    remarks = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_return_detail", x => new { x.tenant_id, x.company_id, x.return_detail_id });
                    table.ForeignKey(
                        name: "fk_return_detail_item",
                        columns: x => new { x.tenant_id, x.company_id, x.item_id },
                        principalTable: "item_master",
                        principalColumns: new[] { "tenant_id", "company_id", "item_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_return_detail_master",
                        columns: x => new { x.tenant_id, x.company_id, x.return_id },
                        principalTable: "supplier_return",
                        principalColumns: new[] { "tenant_id", "company_id", "return_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_return_detail_po",
                        columns: x => new { x.tenant_id, x.company_id, x.po_id },
                        principalTable: "purchase_order",
                        principalColumns: new[] { "tenant_id", "company_id", "po_id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_detail_tax",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<int>(type: "int", nullable: false),
                    sale_detail_id = table.Column<int>(type: "int", nullable: false),
                    tax_id = table.Column<int>(type: "int", nullable: false),
                    tax_percentage = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "admin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_detail_tax", x => new { x.tenant_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_sale_tax",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_id },
                        principalTable: "tax",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_stt_sale",
                        columns: x => new { x.tenant_id, x.company_id, x.sale_detail_id },
                        principalTable: "sale_master_detail",
                        principalColumns: new[] { "tenant_id", "company_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "store_transaction_tax",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    store_tax_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    tax_id = table.Column<int>(type: "int", nullable: false),
                    tax_percentage = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    tax_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "admin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_transaction_tax_1", x => new { x.tenant_id, x.company_id, x.store_tax_id });
                    table.ForeignKey(
                        name: "FK_store_transaction_tax_store_transaction",
                        columns: x => new { x.tenant_id, x.company_id, x.store_id },
                        principalTable: "store_transaction",
                        principalColumns: new[] { "tenant_id", "company_id", "store_id" });
                    table.ForeignKey(
                        name: "FK_store_transaction_tax_tax",
                        columns: x => new { x.tenant_id, x.company_id, x.tax_id },
                        principalTable: "tax",
                        principalColumns: new[] { "tenant_id", "company_id", "tax_id" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ladger_category",
                columns: new[] { "company_id", "id", "tenant_id", "category_code", "category_name", "created_by", "is_active", "sort_order", "updated_by", "updated_on" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1, new Guid("11111111-1111-1111-1111-111111111111"), "AST", "Assets", "system", true, 1, "system", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2, new Guid("11111111-1111-1111-1111-111111111111"), "LIB", "Liabilities", "system", true, 2, "system", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 3, new Guid("11111111-1111-1111-1111-111111111111"), "INC", "Income", "system", true, 3, "system", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 4, new Guid("11111111-1111-1111-1111-111111111111"), "EXP", "Expenses", "system", true, 4, "system", null }
                });

            migrationBuilder.InsertData(
                table: "ledger_master",
                columns: new[] { "company_id", "ledger_id", "tenant_id", "address", "created_by", "created_on", "credit_amount", "debit_amount", "gst_no", "is_active", "ledger_cate_id", "ledger_name", "mobile", "opening_balance", "opening_type", "parent_id", "updated_by", "updated_on" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Account Receivable", null, 0m, "Dr", null, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "Account Payable", null, 0m, "Cr", null, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 3, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "Purchase Account", null, 0m, "Dr", null, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 4, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Current Assets", null, 0m, "Dr", null, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 5, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Current Liabilities", null, 0m, "Cr", null, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 6, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Cash", null, 0m, "Dr", 4, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 7, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Bank", null, 0m, "Dr", 4, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 8, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Upi", null, 0m, "Dr", 4, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 9, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 1, "Cheque-in-hand", null, 0m, "Dr", 4, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 10, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "Sundry Creditors", null, 0m, "Cr", 5, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 11, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "Bills Payable", null, 0m, "Cr", 5, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 12, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "Outstanding Expenses", null, 0m, "Cr", 5, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 13, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "Statutory Payables", null, 0m, "Cr", 5, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 14, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "GST Payable", null, 0m, "Cr", 12, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 15, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "TDS Payable", null, 0m, "Cr", 12, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 16, new Guid("11111111-1111-1111-1111-111111111111"), null, "system", null, 0m, 0m, null, true, 2, "PF / ESI Payable", null, 0m, "Cr", 12, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_master_tenant_id_company_id_ledger_Id",
                table: "customer_master",
                columns: new[] { "tenant_id", "company_id", "ledger_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_item_master_tenant_id_company_id_tax_group_id",
                table: "item_master",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_item_master_tenant_id_company_id_unit_id",
                table: "item_master",
                columns: new[] { "tenant_id", "company_id", "unit_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ledger_detail_tenant_id_company_id_ledger_id",
                table: "ledger_detail",
                columns: new[] { "tenant_id", "company_id", "ledger_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ledger_detail_tenant_id_company_id_sub_ledger_id",
                table: "ledger_detail",
                columns: new[] { "tenant_id", "company_id", "sub_ledger_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ledger_master_tenant_id_company_id_ledger_cate_id",
                table: "ledger_master",
                columns: new[] { "tenant_id", "company_id", "ledger_cate_id" });

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_tenant_id_company_id_project_id",
                table: "purchase_order",
                columns: new[] { "tenant_id", "company_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_tenant_id_company_id_supplier_id",
                table: "purchase_order",
                columns: new[] { "tenant_id", "company_id", "supplier_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_tax_tenant_id_company_id_sale_detail_id",
                table: "sale_detail_tax",
                columns: new[] { "tenant_id", "company_id", "sale_detail_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_tax_tenant_id_company_id_tax_id",
                table: "sale_detail_tax",
                columns: new[] { "tenant_id", "company_id", "tax_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_invoice_payment_tenant_id_company_id_sale_id",
                table: "sale_invoice_payment",
                columns: new[] { "tenant_id", "company_id", "sale_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_item_master_tenant_id_company_id_tax_group_id",
                table: "sale_item_master",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_tenant_id_company_id_customer_id",
                table: "sale_master",
                columns: new[] { "tenant_id", "company_id", "customer_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_tenant_id_company_id_project_id",
                table: "sale_master",
                columns: new[] { "tenant_id", "company_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_tenant_id_company_id_tax_group_id",
                table: "sale_master",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_detail_tenant_id_company_id_item_id",
                table: "sale_master_detail",
                columns: new[] { "tenant_id", "company_id", "item_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_detail_tenant_id_company_id_sale_id",
                table: "sale_master_detail",
                columns: new[] { "tenant_id", "company_id", "sale_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_detail_tenant_id_company_id_tax_group_id",
                table: "sale_master_detail",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sale_master_tax_tenant_id_company_id_sale_id",
                table: "sale_master_tax",
                columns: new[] { "tenant_id", "company_id", "sale_id" });

            migrationBuilder.CreateIndex(
                name: "IX_store_transaction_tenant_id_company_id_item_id",
                table: "store_transaction",
                columns: new[] { "tenant_id", "company_id", "item_id" });

            migrationBuilder.CreateIndex(
                name: "IX_store_transaction_tenant_id_company_id_po_id",
                table: "store_transaction",
                columns: new[] { "tenant_id", "company_id", "po_id" });

            migrationBuilder.CreateIndex(
                name: "IX_store_transaction_tenant_id_company_id_tax_group_id",
                table: "store_transaction",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_store_transaction_tenant_id_company_id_unit_id",
                table: "store_transaction",
                columns: new[] { "tenant_id", "company_id", "unit_id" });

            migrationBuilder.CreateIndex(
                name: "IX_store_transaction_tax_tenant_id_company_id_store_id",
                table: "store_transaction_tax",
                columns: new[] { "tenant_id", "company_id", "store_id" });

            migrationBuilder.CreateIndex(
                name: "IX_store_transaction_tax_tenant_id_company_id_tax_id",
                table: "store_transaction_tax",
                columns: new[] { "tenant_id", "company_id", "tax_id" });

            migrationBuilder.CreateIndex(
                name: "IX_sub_ledger_tenant_id_company_id_ledger_id",
                table: "sub_ledger",
                columns: new[] { "tenant_id", "company_id", "ledger_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_master_tenant_id_company_id_ledger_id",
                table: "supplier_master",
                columns: new[] { "tenant_id", "company_id", "ledger_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_payment_tenant_id_company_id_po_id",
                table: "supplier_payment",
                columns: new[] { "tenant_id", "company_id", "po_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_payment_tenant_id_company_id_supplier_id",
                table: "supplier_payment",
                columns: new[] { "tenant_id", "company_id", "supplier_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_return_tenant_id_company_id_project_id",
                table: "supplier_return",
                columns: new[] { "tenant_id", "company_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_return_tenant_id_company_id_supplier_id",
                table: "supplier_return",
                columns: new[] { "tenant_id", "company_id", "supplier_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_return_detail_tenant_id_company_id_item_id",
                table: "supplier_return_detail",
                columns: new[] { "tenant_id", "company_id", "item_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_return_detail_tenant_id_company_id_po_id",
                table: "supplier_return_detail",
                columns: new[] { "tenant_id", "company_id", "po_id" });

            migrationBuilder.CreateIndex(
                name: "IX_supplier_return_detail_tenant_id_company_id_return_id",
                table: "supplier_return_detail",
                columns: new[] { "tenant_id", "company_id", "return_id" });

            migrationBuilder.CreateIndex(
                name: "IX_tax_tenant_id_company_id_ledger_id",
                table: "tax",
                columns: new[] { "tenant_id", "company_id", "ledger_id" });

            migrationBuilder.CreateIndex(
                name: "IX_tax_group_igst_detail_tenant_id_company_id_tax_group_id",
                table: "tax_group_igst_detail",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_tax_group_igst_detail_tenant_id_company_id_tax_id",
                table: "tax_group_igst_detail",
                columns: new[] { "tenant_id", "company_id", "tax_id" });

            migrationBuilder.CreateIndex(
                name: "IX_tax_group_sgst_detail_tenant_id_company_id_tax_group_id",
                table: "tax_group_sgst_detail",
                columns: new[] { "tenant_id", "company_id", "tax_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_tax_group_sgst_detail_tenant_id_company_id_tax_id",
                table: "tax_group_sgst_detail",
                columns: new[] { "tenant_id", "company_id", "tax_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ledger_detail");

            migrationBuilder.DropTable(
                name: "sale_detail_tax");

            migrationBuilder.DropTable(
                name: "sale_invoice_payment");

            migrationBuilder.DropTable(
                name: "sale_master_tax");

            migrationBuilder.DropTable(
                name: "store_transaction_tax");

            migrationBuilder.DropTable(
                name: "supplier_payment");

            migrationBuilder.DropTable(
                name: "supplier_return_detail");

            migrationBuilder.DropTable(
                name: "tax_group_igst_detail");

            migrationBuilder.DropTable(
                name: "tax_group_sgst_detail");

            migrationBuilder.DropTable(
                name: "voucher_master");

            migrationBuilder.DropTable(
                name: "sub_ledger");

            migrationBuilder.DropTable(
                name: "sale_master_detail");

            migrationBuilder.DropTable(
                name: "store_transaction");

            migrationBuilder.DropTable(
                name: "supplier_return");

            migrationBuilder.DropTable(
                name: "tax");

            migrationBuilder.DropTable(
                name: "sale_item_master");

            migrationBuilder.DropTable(
                name: "sale_master");

            migrationBuilder.DropTable(
                name: "item_master");

            migrationBuilder.DropTable(
                name: "purchase_order");

            migrationBuilder.DropTable(
                name: "customer_master");

            migrationBuilder.DropTable(
                name: "tax_group");

            migrationBuilder.DropTable(
                name: "unit");

            migrationBuilder.DropTable(
                name: "project_master");

            migrationBuilder.DropTable(
                name: "supplier_master");

            migrationBuilder.DropTable(
                name: "ledger_master");

            migrationBuilder.DropTable(
                name: "ladger_category");
        }
    }
}
