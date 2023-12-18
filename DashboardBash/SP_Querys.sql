CREATE PROCEDURE SP_SearchProgram
    @NameProgram NVARCHAR(255),
    @HoraIni DATE
AS
BEGIN
    SELECT *
    FROM tb_Monitoring_Batch
    WHERE NameProgram = @NameProgram
      AND CAST(HoraIni AS DATE) = @HoraIni;
END

Select * from tb_Monitoring_Batch;

DECLARE @NameProgram NVARCHAR(255) = 'Programa 1';
DECLARE @HoraIni DATE = '2023-01-01';

EXEC SP_SearchProgram @NameProgram, @HoraIni;


CREATE PROCEDURE SP_GetPrograms
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TotalCount INT;

    -- Obtener el total de programas
    SELECT @TotalCount = COUNT(*)
    FROM tb_Monitoring_Batch;

    -- Obtener los programas paginados
    SELECT *
    FROM tb_Monitoring_Batch
    ORDER BY HoraIni DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Devolver el total de páginas
    SELECT CEILING(CAST(@TotalCount AS DECIMAL) / @PageSize) AS TotalPages;
END


DECLARE @PageNumber INT = 1;
DECLARE @PageSize INT = 10;

EXEC SP_GetPrograms @PageNumber, @PageSize;


CREATE PROCEDURE SP_GetProgramsForCurrentDay
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentDate DATE;

    -- Obtén la fecha actual del servidor
    SET @CurrentDate = CAST(GETDATE() AS DATE);

    -- Obtén los programas para el día actual
    SELECT
        Id,
        NameProgram,
        Estado,
        HoraIni,
        HoraFin,
        HostName,
        IpAddress
    FROM tb_Monitoring_Batch
    WHERE CAST(HoraIni AS DATE) = @CurrentDate;
END

EXEC SP_GetProgramsForCurrentDay;

INSERT INTO tb_Monitoring_Batch (NameProgram, Estado, HoraIni, HoraFin, HostName, IpAddress)
VALUES
    ('Programa12', 1, GETDATE(), GETDATE(), 'Host1', '192.168.1.1'),
    ('Programa4', 0, GETDATE(), GETDATE(), 'Host2', '192.168.1.2'),
    ('Programa312', 1, GETDATE(), GETDATE(), 'Host3', '192.168.1.3'),
    ('Programa12', 0, GETDATE(), GETDATE(), 'Host4', '192.168.1.4'),
    ('Programa31', 1, GETDATE(), GETDATE(), 'Host5', '192.168.1.5');