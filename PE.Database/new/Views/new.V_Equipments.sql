CREATE   VIEW [new].[V_Equipments]
AS

/*
	select * from new.V_Equipments
*/

SELECT E.EquipmentId, 
       E.EquipmentName, 
       E.EquipmentQuantity, 
       ET.EquipmentTypeName AS EquipmentType, 
       ET.EquipmentMinimumQuantity, 
       ETP.EquipmentTypeName AS ParentEquipmentType
FROM new.MNTEquipments E
     LEFT JOIN new.MNTEquipmentTypes ET ON E.FKEquipmentTypeId = ET.EquipmentTypeId
     LEFT JOIN new.MNTEquipmentTypes ETP ON ET.FKParentEquipmentTypeId = ETP.EquipmentTypeId;