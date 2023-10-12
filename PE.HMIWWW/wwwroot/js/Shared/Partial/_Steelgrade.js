const _Steelgrade = new class {

  GoToSteelgrade(steelgradeId) {
    if (!steelgradeId) {
      console.error('Missing data: steelgradeId');
      return;
    }
    let dataToSend = {
      Id: steelgradeId
    };
    openSlideScreen('Steelgrade', 'ElementDetails', dataToSend);
  }
}