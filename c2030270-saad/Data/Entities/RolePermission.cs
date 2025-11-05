namespace c2030270_saad.Data.Entities
{
    public class RolePermission
    {
        public string RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}