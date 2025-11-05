CREATE TABLE Complaint (
    ComplaintId INT IDENTITY(1,1) PRIMARY KEY,
    TenantId INT NOT NULL,
    UserId INT NOT NULL,
    ReportedById INT NOT NULL,
    AssignedToId INT,
    Status VARCHAR(50) DEFAULT 'Open',
    Priority VARCHAR(20),
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(2000),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    ResolvedAt DATETIME,
    FOREIGN KEY (TenantId) REFERENCES Tenant(TenantId),
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (ReportedById) REFERENCES [User](UserId),
    FOREIGN KEY (AssignedToId) REFERENCES [User](UserId)
);