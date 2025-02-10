﻿using Dapper;
using dotent.Controllers;
using Npgsql;

namespace dotent.Repositories;

public class MemberRepository
{
    private readonly IConfiguration _configuration;

    public MemberRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        const string selectQuery = $"""
                                    select * from members where id = @id 
                                    """;
        var connectionString = _configuration.GetConnectionString("Default");

        await using var connection = new NpgsqlConnection(connectionString);

        var member = await connection.QueryFirstOrDefaultAsync<Member>(selectQuery,
            new
            {
                id = id
            });

        return member;
    }

    public async Task<List<Member>> GetAllAsync(string firstname)
    {
        const string selectQuery = $"""
                                    select * from members where firstname = @firstname  
                                    """;
        var connectionString = _configuration.GetConnectionString("Default");

        await using var connection = new NpgsqlConnection(connectionString);

        var members = (await connection.QueryAsync<Member>(selectQuery,
            new
            {
                firstname = firstname
            })).ToList();

        return members;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection("Your_Connection_String");
        var query = "DELETE FROM Members WHERE Id = @Id";

        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0; // Returns true if deletion was successful
    }
}