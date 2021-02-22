using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

namespace KSM.DAO.Models
{
    internal class DaoFactory
    {
        private static object syncLock = new object();
        private static ISqlMapper mapper = null;

        public static ISqlMapper Instance
        {
            get
            {
                try
                {
                    if (mapper == null)
                    {
                        lock (syncLock)
                        {
                            if (mapper == null)
                            {
                                DomSqlMapBuilder dom = new DomSqlMapBuilder();

                                XmlDocument sqlMapConfig = Resources.GetEmbeddedResourceAsXmlDocument("Config.SqlMap.config, KSM.DAO");

                                mapper = dom.Configure(sqlMapConfig);
                            }
                        }
                    }

                    return mapper;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
