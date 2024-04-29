USE Tournaments;

-- Phantom Reads Transaction 2: select + delay + select
-- Order of the operations: select - insert - select
go
CREATE PROCEDURE Phantom_Reads_T2 AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ		-- !!!!!!!!
	BEGIN TRAN
	BEGIN TRY
		SELECT * FROM Tournaments
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('SELECT', 'Tournaments', CURRENT_TIMESTAMP)
		WAITFOR DELAY '00:00:10'
		SELECT * FROM Tournaments
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('SELECT', 'Tournaments', CURRENT_TIMESTAMP)
		COMMIT TRAN
		SELECT 'Transaction committed' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked' AS [Message]
	END CATCH
END

EXECUTE Phantom_Reads_T2;

-- Corrected:
go
CREATE PROCEDURE Phantom_Reads_T2_Corrected AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	BEGIN TRAN
	BEGIN TRY
		SELECT * FROM Tournaments
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('SELECT', 'Tournaments', CURRENT_TIMESTAMP)
		WAITFOR DELAY '00:00:10'
		SELECT * FROM Tournaments
			INSERT INTO LogTable(operationType, tableName, executionTime) VALUES ('SELECT', 'Tournaments', CURRENT_TIMESTAMP)
		COMMIT TRAN
		SELECT 'Transaction committed' AS [Message]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT 'Transaction rollbacked' AS [Message]
	END CATCH
END

EXECUTE Phantom_Reads_T2_Corrected;