/*using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

/// <summary>
/// I'm sorry. I tried, I really did
/// </summary>
public class SQLiteService
{
    public string DataBaseName;
    public List<string> TableNames { get; private set; }

    public SQLiteService(string dataBaseName)
    {
        DataBaseName = dataBaseName;
    }

    private void SendQuery(string query)
    {
        SqliteConnection connection = new SqliteConnection(DataBaseName);
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
        connection.Close();
    }

    public string CreateTableQuery(string tableName, Dictionary<string, string> fields)
    {
        if (tableName == null || tableName == "")
            throw new ArgumentNullException("tableName should not be empty");
        TableNames.Add(tableName);
        List<string> fieldsString = new List<string>();
        foreach (var field in fields)
            fieldsString.Add($"{field.Key} {field.Value}");
        string query = $"CREATE TABLE IF NOT EXISTS {tableName} ({string.Join(", ", fieldsString)});";
        SendQuery(query);
        return query;
    }

    public void CreateTable(string tableName, Dictionary<string, string> fields)
    {
        SendQuery(CreateTableQuery(tableName, fields));
    }

    /// <summary>
    /// Good luck
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="selected"></param>
    /// <param name="rowFilter"></param>
    /// <param name="orderByField"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="groupBy"></param>
    /// <param name="groupFilter"></param>
    /// <remarks>I'm not sure of what I did</remarks>
    public string SelectQuery(string tableName, string selected = "*", string rowFilter = null, string orderByField = null, int limit = -1, int offset = -1, string groupBy = null, string groupFilter = null)
    {
        string conditionString = rowFilter == null ? "" : $" WHERE {rowFilter}";
        string orderByFieldString = orderByField == null ? "" : $" ORDER BY {orderByField}";
        string offsetString = offset == -1 ? "" : $" OFFSET {offset}";
        string limitString = limit == -1 ? "" : $" LIMIT {limit}{offsetString}";
        string groupByString = groupBy == null ? "" : $" GROUP BY {groupBy}";
        string groupFilterString = groupFilter == null ? "" : $" HAVING {groupFilter}";
        return $"SELECT {selected} FROM {tableName}{conditionString}{orderByFieldString}{limitString}{offsetString}{groupByString}";
    }

    public void Select(string tableName, string selected = "*", string rowFilter = null, string orderByField = null, int limit = -1, int offset = -1, string groupBy = null, string groupFilter = null)
    {
        SendQuery(SelectQuery(tableName, selected, rowFilter, orderByField, limit, offset, groupBy, groupFilter));
    }

    public string InsertQuery(string tableName, Dictionary<string, string> values)
    {
        return $"INSERT INTO {tableName} VALUES {values}";
    }

    public void Insert(string tableName, Dictionary<string, string> values)
    {
        SendQuery(InsertQuery(tableName, values));
    }
}
*/