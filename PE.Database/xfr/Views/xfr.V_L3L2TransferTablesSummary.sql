

CREATE   VIEW [xfr].[V_L3L2TransferTablesSummary]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY TransferTableName), 0) AS OrderSeq, 
          TransferTableName, 
          StatusNew, 
          StatusInProc, 
          StatusOK, 
          StatusValErr, 
          StatusProcErr
   FROM
   (
       SELECT 'L3 to L2 WorkOrder Definitions' AS TransferTableName, 
              1 Sorting, 
              SUM(CASE
                      WHEN CommStatus = 0
                      THEN 1
                      ELSE 0
                  END) AS StatusNew, 
              SUM(CASE
                      WHEN CommStatus = 1
                      THEN 1
                      ELSE 0
                  END) AS StatusInProc, 
              SUM(CASE
                      WHEN CommStatus = 2
                      THEN 1
                      ELSE 0
                  END) AS StatusOK, 
              SUM(CASE
                      WHEN CommStatus = -1
                      THEN 1
                      ELSE 0
                  END) AS StatusValErr, 
              SUM(CASE
                      WHEN CommStatus = -2
                      THEN 1
                      ELSE 0
                  END) AS StatusProcErr
       FROM xfr.L3L2WorkOrderDefinition AS L3WOD
       WHERE L3WOD.IsUpdated = 0
       UNION ALL
       SELECT 'L2 to L3 WorkOrder Reports' AS TransferTableName, 
              2 Sorting, 
              SUM(CASE
                      WHEN CommStatus = 0
                      THEN 1
                      ELSE 0
                  END) AS StatusNew, 
              SUM(CASE
                      WHEN CommStatus = 1
                      THEN 1
                      ELSE 0
                  END) AS StatusInProc, 
              SUM(CASE
                      WHEN CommStatus = 2
                      THEN 1
                      ELSE 0
                  END) AS StatusOK, 
              SUM(CASE
                      WHEN CommStatus = -1
                      THEN 1
                      ELSE 0
                  END) AS StatusValErr, 
              SUM(CASE
                      WHEN CommStatus = -2
                      THEN 1
                      ELSE 0
                  END) AS StatusProcErr
       FROM xfr.L2L3WorkOrderReport AS L2WOR
       WHERE 1 = 1  
       UNION ALL
       SELECT 'L2 to L3 Product Reports' AS TransferTableName, 
              2 Sorting, 
              SUM(CASE
                      WHEN CommStatus = 0
                      THEN 1
                      ELSE 0
                  END) AS StatusNew, 
              SUM(CASE
                      WHEN CommStatus = 1
                      THEN 1
                      ELSE 0
                  END) AS StatusInProc, 
              SUM(CASE
                      WHEN CommStatus = 2
                      THEN 1
                      ELSE 0
                  END) AS StatusOK, 
              SUM(CASE
                      WHEN CommStatus = -1
                      THEN 1
                      ELSE 0
                  END) AS StatusValErr, 
              SUM(CASE
                      WHEN CommStatus = -2
                      THEN 1
                      ELSE 0
                  END) AS StatusProcErr
       FROM xfr.L2L3ProductReport AS L2PR
       WHERE 1 = 1
   ) AS QRY;