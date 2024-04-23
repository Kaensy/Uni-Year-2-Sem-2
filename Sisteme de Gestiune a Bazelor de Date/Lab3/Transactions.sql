CREATE TABLE LogTable
(
	id INT IDENTITY,
	operationType VARCHAR(20),
	tableName VARCHAR(20),
	executionTime DATETIME,
	CONSTRAINT pk_LogTable PRIMARY KEY(id)
)

go
create or alter function ValidateTournaments(@name varchar(100), @StartDate date, @EndDate date,@Location varchar(100), @PrizePool int, @OrganizerId int, @GameId int) 
returns varchar(200) as
	begin
	DECLARE @errorMessage VARCHAR(200)
	SET @errorMessage = ''
	if ISNULL(@name, '') = '' OR LEN(@name) = 0
		SET @errorMessage = @errorMessage + 'Nume invalid. '
	if ISNULL(@StartDate, '') = '' 
		SET @errorMessage = @errorMessage + 'Data Inceput invalida. '
	if ISNULL(@EndDate, '') = '' OR @EndDate < @StartDate
		SET @errorMessage = @errorMessage + 'Data Sfarsit invalida. '
	if ISNULL(@Location, '') = '' OR LEN(@Location) = 0 
		SET @errorMessage = @errorMessage + 'Locatie invalida. '
	if ISNULL(@PrizePool, 0) <= 0 
		SET @errorMessage = @errorMessage + 'Prize Pool invalid. '
	if not exists (select * from Organizers where OrganizerID = @OrganizerId )
		SET @errorMessage = @errorMessage + 'Organizator invalid. '
	if not exists (select * from Games where GameID = @GameId )
		SET @errorMessage = @errorMessage + 'Joc invalid. '
	return @errorMessage
	end

go
create or alter function ValidateSponsors(@name varchar(100), @field varchar(100)) 
returns varchar(200) as
	begin
	DECLARE @errorMessage VARCHAR(200)
	SET @errorMessage = ''
	if ISNULL(@name, '') = '' OR LEN(@name) = 0
		SET @errorMessage = @errorMessage + 'Nume invalid. '
	if ISNULL(@field, '') = '' OR LEN(@field) = 0 
		SET @errorMessage = @errorMessage + 'Field invalid. '
	return @errorMessage
	end

go
create or alter function ValidateTournamentsSponsors(@TournamentId int, @SponsorId int) 
returns varchar(200) as
	begin
	DECLARE @errorMessage VARCHAR(200)
	SET @errorMessage = ''
	if not exists (select * from Tournaments where TournamentID = @TournamentId )
		SET @errorMessage = @errorMessage + 'Nu exista turneu cu ID-ul ' + CONVERT(VARCHAR, @TournamentId) + '. '
	if not exists (select * from Sponsors where SponsorID = @SponsorId )
		SET @errorMessage = @errorMessage + 'Nu exista sponsor cu ID-ul ' + CONVERT(VARCHAR, @SponsorId) + '. '
	IF (EXISTS(SELECT TournamentID, SponsorID FROM TournamentsSponsors WHERE TournamentID = TournamentID AND SponsorID = @SponsorId))
		SET @errorMessage = @errorMessage + 'Valorile deja exista in tabel.'
	return @errorMessage
	end

go
create or alter procedure InsertIntoTables(@nameTournament varchar(100), @StartDate date, @EndDate date,@Location varchar(100), @PrizePool int, @OrganizerId int, @GameId int,@nameSponsor varchar(100), @field varchar(100))
AS
Begin
	begin tran
	begin try
		declare @errorMessage varchar(200)
		set @errorMessage = dbo.ValidateTournaments(@nameTournament,@StartDate,@EndDate,@Location,@PrizePool,@OrganizerId,@GameId)
		if(@errorMessage != '')
		begin
			raiserror(@errorMessage, 14,1)
		end

		insert into Tournaments(TournamentName,StartDate,EndDate,TournamentLocation,PrizePool,OrganizerID,GameID)
		Values(@nameTournament,@StartDate,@EndDate,@Location,@PrizePool,@OrganizerId,@GameId)

		insert into LogTable(operationType,tableName,executionTime) Values('INSERT', 'Tournaments', CURRENT_TIMESTAMP)

		set @errorMessage = dbo.ValidateSponsors(@nameSponsor,@field)
		if(@errorMessage != '')
		begin
			raiserror(@errorMessage, 14,1)
		end

		insert into Sponsors(SponsorName,Field) Values(@nameSponsor,@field)
		insert into LogTable(operationType,tableName,executionTime) Values('INSERT', 'Sponsors', CURRENT_TIMESTAMP)

		declare @idTournament int, @idSponsor int
		set @idTournament = (select MAX(TournamentID) from Tournaments)
		insert into LogTable(operationType,tableName,executionTime) Values('SELECT', 'Tournaments', CURRENT_TIMESTAMP)
		set @idSponsor = (select MAX(SponsorID) from Sponsors)
		insert into LogTable(operationType,tableName,executionTime) Values('SELECT', 'Sponsors', CURRENT_TIMESTAMP)
		
		print @idTournament
		print @idSponsor

		set @errorMessage = dbo.ValidateTournamentsSponsors(@idTournament,@idSponsor)
		if(@errorMessage != '')
		begin
			raiserror(@errorMessage, 14,1)
		end
		
		insert into TournamentsSponsors(TournamentID,SponsorID) Values(@idTournament,@idSponsor)
		insert into LogTable(operationType,tableName,executionTime) Values('INSERT', 'TournamentsSponsors', CURRENT_TIMESTAMP)
		commit tran

		select 'Transaction committed'
	end try
	begin catch
		rollback tran
		select 'Transaction rollbacked'
	end catch
end
go

-- success
execute dbo.InsertIntoTables 'NumeTurneuTest', '2000-01-01', '2001-01-01', 'LocationTurneuTest', 500000, 1, 1, 'NumeSponsorTest', 'NumeFieldTest'
select * from Tournaments
select * from Sponsors
select * from TournamentsSponsors
select * from LogTable

-- failure
execute dbo.InsertIntoTables '', '2000-01-01', '2001-01-01', 'LocationTurneuTest', 500000, 3, 1, 'NumeSponsorTest', 'NumeFieldTest'
select * from Tournaments
select * from Sponsors
select * from TournamentsSponsors
select * from LogTable


go
create or alter procedure InsertIntoTablesV2(@nameTournament varchar(100), @StartDate date, @EndDate date,@Location varchar(100), @PrizePool int, @OrganizerId int, @GameId int,@nameSponsor varchar(100), @field varchar(100))
AS
Begin
	
	declare @error int
	set @error = 0

	begin tran
	begin try
		declare @errorMessage varchar(200)
		set @errorMessage = dbo.ValidateTournaments(@nameTournament,@StartDate,@EndDate,@Location,@PrizePool,@OrganizerId,@GameId)
		if(@errorMessage != '')
		begin
			raiserror(@errorMessage, 14,1)
		end

		insert into Tournaments(TournamentName,StartDate,EndDate,TournamentLocation,PrizePool,OrganizerID,GameID)
		Values(@nameTournament,@StartDate,@EndDate,@Location,@PrizePool,@OrganizerId,@GameId)

		insert into LogTable(operationType,tableName,executionTime) Values('INSERT', 'Tournaments', CURRENT_TIMESTAMP)

		commit tran
		select 'Transaction committed for table Tournaments'
	end try
	begin catch
		rollback tran
		select 'Transaction rollbacked for table Tournaments'
		insert into LogTable(operationType,tableName,executionTime) Values('ROLLBACK', 'Tournaments', CURRENT_TIMESTAMP)
		set @error = 1
	end catch

	begin tran
	begin try
		set @errorMessage = dbo.ValidateSponsors(@nameSponsor,@field)
		if(@errorMessage != '')
		begin
			raiserror(@errorMessage, 14,1)
		end

		insert into Sponsors(SponsorName,Field) Values(@nameSponsor,@field)
		insert into LogTable(operationType,tableName,executionTime) Values('INSERT', 'Sponsors', CURRENT_TIMESTAMP)
		
		commit tran
		select 'Transaction committed for table Sponsors'
	end try
	begin catch
		rollback tran
		select 'Transaction rollbacked for table Sponsors'
		insert into LogTable(operationType,tableName,executionTime) Values('ROLLBACK', 'Sponsors', CURRENT_TIMESTAMP)
	end catch

	if (@error != 0)
		return

	begin tran
	begin try
		declare @idTournament int, @idSponsor int
		set @idTournament = (select MAX(TournamentID) from Tournaments)
		insert into LogTable(operationType,tableName,executionTime) Values('SELECT', 'Tournaments', CURRENT_TIMESTAMP)
		set @idSponsor = (select MAX(SponsorID) from Sponsors)
		insert into LogTable(operationType,tableName,executionTime) Values('SELECT', 'Sponsors', CURRENT_TIMESTAMP)
		
		set @errorMessage = dbo.ValidateTournamentsSponsors(@idTournament,@idSponsor)
		if(@errorMessage != '')
		begin
			raiserror(@errorMessage, 14,1)
		end
		
		insert into TournamentsSponsors(TournamentID,SponsorID) Values(@idTournament,@idSponsor)
		insert into LogTable(operationType,tableName,executionTime) Values('INSERT', 'TournamentsSponsors', CURRENT_TIMESTAMP)
		
		commit tran
		select 'Transaction committed for table TournamentsSponsors'
	end try
	begin catch
		rollback tran
		select 'Transaction rollbacked for table TournamentsSponsors'
		insert into LogTable(operationType,tableName,executionTime) Values('ROLLBACK', 'TournamentsSponsors', CURRENT_TIMESTAMP)
		set @error = 1
	end catch
end
go

-- success
execute dbo.InsertIntoTablesV2 'NumeTurneuTest', '2000-01-01', '2001-01-01', 'LocationTurneuTest', 500000, 1, 1, 'NumeSponsorTest', 'NumeFieldTest'
select * from Tournaments
select * from Sponsors
select * from TournamentsSponsors
select * from LogTable

-- failure
execute dbo.InsertIntoTablesV2 'NumeTurneuTest', '2000-01-01', '2001-01-01', 'LocationTurneuTest', 500000, 1, 1, 'NumeSponsorTest', ''
select * from Tournaments
select * from Sponsors
select * from TournamentsSponsors
select * from LogTable
