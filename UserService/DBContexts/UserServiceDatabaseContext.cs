﻿using Microsoft.EntityFrameworkCore;
using UserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace UserService.DBContexts
{
    /// <summary>
    /// Context of the database, used to communicate with the database.
    /// </summary>
    public class UserServiceDatabaseContext : DbContext
    {
        /// <summary>
        /// Constructor of the UserServiceDatabaseContext class
        /// </summary>
        public UserServiceDatabaseContext()
        {
        }

        /// <summary>
        /// Constructor of the UserServiceDatabaseContext class with options, used for Unittesting
        /// Database options can be given, to switch between local and remote database
        /// </summary>
        /// <param name="options">Database options</param>
        public UserServiceDatabaseContext(DbContextOptions<UserServiceDatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet for the User class, A DbSet represents the collection of all entities in the context. 
        /// DbSet objects are created from a DbContext using the DbContext.Set method.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet for the Role class, A DbSet represents the collection of all entities in the context. 
        /// DbSet objects are created from a DbContext using the DbContext.Set method.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// DbSet for the Permission class, A DbSet represents the collection of all entities in the context. 
        /// DbSet objects are created from a DbContext using the DbContext.Set method.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// OnConfiguring builds the connection between the database and the API using the given connection string
        /// </summary>
        /// <param name="optionsBuilder">Used for adding options to the database to configure the connection.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"databases\aci_db_string.txt");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Could not find database string!");
                }

                optionsBuilder.UseSqlServer(File.ReadAllText(path).Replace("DATABASE_NAME", "UserService"));
            }

            base.OnConfiguring(optionsBuilder);
        }

    }
}