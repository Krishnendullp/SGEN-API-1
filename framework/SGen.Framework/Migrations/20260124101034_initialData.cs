using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGen.Framework.Migrations
{
    /// <inheritdoc />
    public partial class initialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 1, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_name" },
                values: new object[] { true, "Current Assets" });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 2, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_name" },
                values: new object[] { true, "Current Liabilities" });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 4, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_cate_id", "ledger_name", "parent_id" },
                values: new object[] { true, 2, "Account Payable", 2 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 5, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_name", "parent_id" },
                values: new object[] { true, "Account Receivable", 1 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 6, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 7, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 8, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 9, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 10, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "parent_id" },
                values: new object[] { true, 2 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 11, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 12, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "parent_id" },
                values: new object[] { true, 2 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 13, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "parent_id" },
                values: new object[] { true, 2 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 14, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "is_control_ledger",
                value: true);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 15, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "is_control_ledger",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 1, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_name" },
                values: new object[] { false, "Account Receivable" });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 2, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_name" },
                values: new object[] { false, "Account Payable" });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 4, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_cate_id", "ledger_name", "parent_id" },
                values: new object[] { false, 1, "Current Assets", null });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 5, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "ledger_name", "parent_id" },
                values: new object[] { false, "Current Liabilities", null });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 6, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 7, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 8, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 9, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 10, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "parent_id" },
                values: new object[] { false, 5 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 11, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "parent_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 12, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "parent_id" },
                values: new object[] { false, 5 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 13, new Guid("11111111-1111-1111-1111-111111111111") },
                columns: new[] { "is_control_ledger", "parent_id" },
                values: new object[] { false, 5 });

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 14, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "is_control_ledger",
                value: false);

            migrationBuilder.UpdateData(
                table: "ledger_master",
                keyColumns: new[] { "company_id", "ledger_id", "tenant_id" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 15, new Guid("11111111-1111-1111-1111-111111111111") },
                column: "is_control_ledger",
                value: false);
        }
    }
}
