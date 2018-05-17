using System;

namespace Foundation.BusinessAdmin
{
    public class PermissionCategory
    {
        public PermissionCategory(int id, int parentId, string name)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
        }
        public int Id { get { return id; } }

        public int ParentId { get { return parentId; } }

        public string Name { get { return name; } }

        private int id;
        private int parentId;
        private string name;
    }
}
