using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDBContext context)
        {
           context.Genres.AddRange(
                        new Genre{
                            Name = "PersonalGrowth",

                        },
                        new Genre{
                            Name = "SciFi",
                            
                        },
                        new Genre{
                            Name = "Novel",
                            
                        }
                    );
        }
    }
}