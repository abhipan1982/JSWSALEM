CREATE   VIEW [tool].DFLT_PRMSteelgrades
WITH SCHEMABINDING
AS
     SELECT IsDefault
     FROM dbo.PRMSteelgrades
     WHERE IsDefault = 1;
GO
CREATE UNIQUE CLUSTERED INDEX [UQ_ISDefault]
    ON [tool].[DFLT_PRMSteelgrades]([IsDefault] ASC);

