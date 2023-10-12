const _Product = new class {

  GoToProduct(productId) {
    if (!productId) {
      console.error('Missing data: productId');
      return;
    }
    let dataToSend = {
      productId: productId
    };
    openSlideScreen('Products', 'ElementDetails', dataToSend, Translations["NAME_Product"]);
  }

  BundleCreatePopup(workOrderId) {
    if (!workOrderId) {
      InfoMessage(Translations["MESSAGE_SelectElement"]);
      return;
    }

    OpenInPopupWindow({
      controller: "Products", method: "BundleCreatePopup", width: 450, data: { workOrderId: workOrderId }
    });
  }
}
