USE Tournaments;

-- Non-Repeatable Reads Transaction 1: delay + update + commit
go
CREATE or ALTER PROCEDURE Non_Repeatable_Reads_T1 AS
BEGIN
	BEGIN TRAN
	BEGIN TRY
		WAITFOR DELAY '00:00:05'
		UPDATE Tournaments SET PrizePool = 777 WHERE TournamentName = 'Test'
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('UPDATE', 'Tournaments', CURRENT_TIMESTAMP)
		COMMIT TRAN
		SELECT 'Transaction committed' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked' AS [Message]
	END CATCH
END

EXECUTE Non_Repeatable_Reads_T1;

