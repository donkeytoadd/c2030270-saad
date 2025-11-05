CREATE TABLE RolePermission (
    RolePermissionId VARCHAR(50) PRIMARY KEY,
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    AddedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
    FOREIGN KEY (PermissionId) REFERENCES Permission(PermissionId)
);