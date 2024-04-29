use Tournaments

-- Dirty Reads Transaction 1: update + delay + rollback
go
CREATE or alter PROCEDURE Dirty_Reads_T1 AS
BEGIN
	BEGIN TRAN
	BEGIN TRY
		UPDATE Tournaments SET PrizePool = 777 WHERE TournamentName = 'Test'
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('UPDATE', 'Tournaments', CURRENT_TIMESTAMP)
		WAITFOR DELAY '00:00:05'
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked - good!' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked - bad!' AS [Message]
	END CATCH
END


EXECUTE Dirty_Reads_T1;

