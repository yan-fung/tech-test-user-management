using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyApp.Models;

namespace MyApp.Data
{
    public class DataContext
    {
        // Databas tables
        private List<User> Users { get; set; }


        public DataContext()
        {
            // On startup we want to seed the data in
            Seed();
        }


        /// <summary>
        /// Seed the default data for the app
        /// </summary>
        private void Seed()
        {
            Users = new List<User>
            {
                new User { Id = 1, Forename = "Grant", Surname = "Cooper", Email = "grant.cooper@example.com", IsActive = true },
                new User { Id = 2, Forename = "Tom", Surname = "Gathercole", Email = "tom.gathercole@example.com", IsActive = true },
                new User { Id = 3, Forename = "Mark", Surname = "Edmondson", Email = "mark.edmondson@example.com", IsActive = true },
                new User { Id = 4, Forename = "Graham", Surname = "Clark", Email = "graham.clark@example.com", IsActive = true }
            };
        }



        public List<TEntity> Set<TEntity>() where TEntity : class
        {
            var propertyInfo = PropertyInfos.FirstOrDefault(p => p.PropertyType == typeof(List<TEntity>));


            if (propertyInfo != null)
            {
                // Get the List<T> from 'this' Context instance
                var x = propertyInfo.GetValue(this, null) as List<TEntity>;

                return x;
            }

            throw new Exception("Type collection not found");
        }
        private IEnumerable<PropertyInfo> _propertyInfos;
        private IEnumerable<PropertyInfo> PropertyInfos
        {
            get
            {
                return _propertyInfos ??
                        (_propertyInfos = GetType()
                                            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                            .Where(p => p.PropertyType.IsGenericType &&
                                                        p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)));
            }
        }
    }
}