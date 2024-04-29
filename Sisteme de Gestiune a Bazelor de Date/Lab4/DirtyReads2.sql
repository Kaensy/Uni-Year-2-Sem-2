USE Tournaments;

-- Dirty Reads Transaction 2: select + delay + select
-- Order of the operations: update - select - rollback - select
go
CREATE or ALTER PROCEDURE Dirty_Reads_T2 AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	-- !!!!!!!!!!!
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

EXECUTE Dirty_Reads_T2;

-- Corrected Version:
go
CREATE or ALTER PROCEDURE Dirty_Reads_T2_Corrected AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
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

EXECUTE Dirty_Reads_T2_Corrected;