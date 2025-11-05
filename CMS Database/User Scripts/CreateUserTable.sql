-- User table
CREATE TABLE [User] (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    TenantId INT NOT NULL,
    RoleId INT NOT NULL,
    FName VARCHAR(100) NOT NULL,
    LName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    ContactNumber VARCHAR(20),
    PasswordHash VARCHAR(255) NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TenantId) REFERENCES Tenant(TenantId),
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);