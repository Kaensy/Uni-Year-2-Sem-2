USE Tournaments;

-- Deadlock Transaction 1: update on Tournaments + delay + update on Organizers
go
CREATE or ALTER PROCEDURE Deadlock_T1 AS
BEGIN
	BEGIN TRAN
	BEGIN TRY
		UPDATE Tournaments SET PrizePool = 888 where TournamentName = 'Deadlock Tournament'
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('UPDATE', 'Tournaments', CURRENT_TIMESTAMP)
		WAITFOR DELAY '00:00:05'
		UPDATE Organizers SET OrganizerName = 'DEADLOCK' WHERE OrganizerName = 'Deadlock'
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('UPDATE', 'Organizers', CURRENT_TIMESTAMP)
		COMMIT TRAN
		SELECT 'Transaction committed' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked' AS [Message]
	END CATCH
END

EXECUTE Deadlock_T1;
