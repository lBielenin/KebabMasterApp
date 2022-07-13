namespace KebabInfrastructure.Context
{
    internal static class ADONetQueries
    {
        internal static string OrderNumberByIdQuery =
            @"SELECT TOP 1 [OrderNumber] FROM [dbo].[Orders]WHERE OrderId = @orderId";
    }
}
