using System;

namespace Foundation.BusinessAdmin
{
    public class Permission
    {
        public static string TYPE_AND = "and";
        public static string TYPE_OR = "or";

        public Permission(int id, int parentId, string type, string name, string code)
        {
            this.id = id;
            this.parentId = parentId;
            this.type = type;
            this.name = name;
            this.code = code;
        }
        public int Id { get { return id; } }

        public int ParentId { get { return parentId; } }

        public string Type { get { return type; } }

        public string Name { get { return name; } }

        public string Code { get { return code; } }

        private int id;
        private int parentId;
        private string type;
        private string name;
        private string code;
    }
}
