USE Tournaments;

-- Deadlock Transaction 2 : update on Organizers + delay + update on Tournaments
go
CREATE or ALTER PROCEDURE Deadlock_T2 AS
BEGIN
	SET DEADLOCK_PRIORITY HIGH
	-- SET DEADLOCK_PRIORITY LOW
	BEGIN TRAN
	BEGIN TRY
		UPDATE Organizers SET OrganizerName = 'DEADLOCK' WHERE OrganizerName = 'Deadlock'
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('UPDATE', 'Organizers', CURRENT_TIMESTAMP)
		WAITFOR DELAY '00:00:05'
		UPDATE Tournaments SET PrizePool = 888 where TournamentName = 'Deadlock Tournament'
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('UPDATE', 'Tournaments', CURRENT_TIMESTAMP)
		COMMIT TRAN
		SELECT 'Transaction committed' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked' AS [Message]
	END CATCH
END

EXECUTE Deadlock_T2;