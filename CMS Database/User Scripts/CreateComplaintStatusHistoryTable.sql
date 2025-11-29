CREATE TABLE ComplaintStatusHistory (
    ComplaintStatusHistoryId INT IDENTITY(1,1) PRIMARY KEY,
    ComplaintId INT NOT NULL,
    OldStatus VARCHAR(50) NULL,
    NewStatus VARCHAR(50) NOT NULL,
    ChangedAt DATETIME NOT NULL DEFAULT GETDATE(),
    ChangedBy INT NOT NULL

    CONSTRAINT FK_ComplaintStatusHistory_Complaint 
        FOREIGN KEY (ComplaintId) REFERENCES Complaint(ComplaintId)
);
