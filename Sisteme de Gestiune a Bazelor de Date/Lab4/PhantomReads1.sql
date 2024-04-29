USE Tournaments;

-- Phantom Reads Transaction 1: delay + insert + commit
go
CREATE or ALTER PROCEDURE Phantom_Reads_T1 AS
BEGIN
	BEGIN TRAN
	BEGIN TRY
		WAITFOR DELAY '00:00:05'
		INSERT INTO Tournaments(TournamentName,StartDate,EndDate,TournamentLocation,PrizePool,OrganizerID,GameID) 
			VALUES ('New Test', '2000-01-01', '2001-01-01','T Location', 250000,1,1)
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('INSERT', 'Tournaments', CURRENT_TIMESTAMP)
		COMMIT TRAN
		SELECT 'Transaction committed' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked' AS [Message]
	END CATCH
END

EXECUTE Phantom_Reads_T1;

