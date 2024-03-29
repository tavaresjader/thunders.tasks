CREATE TABLE Task (
	Id INT auto_increment NOT NULL,
	Title varchar(100) NOT NULL,
	Description varchar(100) NULL,
	EstimateStartDate DATETIME NULL,
	EstimateEndDate DATETIME NULL,
	Deleted BIT NOT NULL,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NULL,
	CONSTRAINT PK_TaskId PRIMARY KEY (Id)
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_0900_ai_ci;
