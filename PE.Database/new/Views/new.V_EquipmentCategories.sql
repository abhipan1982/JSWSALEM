CREATE   VIEW new.V_EquipmentCategories
AS

/*
	select * from new.V_EquipmentCategories
*/

SELECT ECG.EquipmentCategoryGroupName AS EquipmentGroup, 
       EC.EquipmentCategoryName AS EquipmentCategory, 
       E.EquipmentName
FROM new.MNTEquipmentCategories EC
     INNER JOIN new.MNTEquipmentCategoryGroups ECG ON EC.FKEquipmentCategoryGroupId = ECG.EquipmentCategoryGroupId
     INNER JOIN new.MNTEquipmentToEquipmentCategories ETEC ON EC.EquipmentCategoryId = ETEC.FKEquipmentCategoryId
     INNER JOIN new.MNTEquipments E ON ETEC.FKEquipmentId = E.EquipmentId;