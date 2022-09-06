using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Atom.Data.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private class MappedType
        {
            public Type First { get; set; }

            public Type Second { get; set; }

            public MappedType(Type first, Type second)
            {
                First = first; 
                Second = second;
            }
        }

        public AutoMapperProfile()
        {
            Type[] entities = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Atom.Data.Entity.");
            Type[] models = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Atom.Data.Model.");

            List<MappedType> mapped = MapTypes(entities, models);

            foreach (var t in mapped)
            {
                CreateMap(t.First, t.Second).IncludeAllDerived().ReverseMap();
            }
        }

        private List<MappedType> MapTypes(Type[] entities, Type[] models)
        {
            List<MappedType> map = new();

            foreach(var entity in entities)
            {
                string entName = entity.Name;

                string entityName = entName.Substring(0, entName.LastIndexOf("Entity"));

                string entityNameSpace = entity.Namespace;

                string ens = entityNameSpace.Substring(entityNameSpace.LastIndexOf("."));

                Type model = null;

                foreach (var m in models)
                {
                    string modelName = m.Name.Substring(0, m.Name.LastIndexOf("Model"));

                    string modelNS = m.Namespace;

                    string mns = modelNS.Substring(modelNS.LastIndexOf("."));

                    if (modelName == entityName && mns == ens)
                    {
                        model = m;
                        break;
                    }
                }

                if(model != null)
                {
                    MappedType mt = new(entity, model);

                    map.Add(mt);
                }
            }

            return map;
        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(x => x.FullName.StartsWith(nameSpace)).ToArray();
        }
    }
}
