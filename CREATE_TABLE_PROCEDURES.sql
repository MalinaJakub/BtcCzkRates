CREATE TABLE BitcoinData (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PriceEUR DECIMAL(18, 2),
    PriceCZK DECIMAL(18, 2),
    Timestamp DATETIME DEFAULT GETDATE(),
    Note NVARCHAR(255)
);

GO
-- Uložení dat do tabulky
CREATE PROCEDURE stp_SaveBitcoinData
    @PriceEUR DECIMAL(18, 2),
    @PriceCZK DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO BitcoinData (PriceEUR, PriceCZK, Note)
    VALUES (@PriceEUR, @PriceCZK, NULL);
END
GO
-- Načtení dat z tabulky
CREATE PROCEDURE stp_LoadBitcoinData
AS
BEGIN
    SELECT Id, PriceEUR, PriceCZK, Note, [Timestamp] FROM BitcoinData ORDER BY Id DESC;
END
GO

-- Aktualizace poznámky podle ID
CREATE PROCEDURE stp_UpdateNoteById
    @Id INT,
    @Note NVARCHAR(255)
AS
BEGIN
    UPDATE BitcoinData
    SET Note = @Note
    WHERE Id = @Id;
END
GO

-- Smazání záznamu podle ID
CREATE PROCEDURE stp_DeleteData
    @Id INT
AS
BEGIN
    DELETE FROM BitcoinData
    WHERE Id = @Id;
END
GO