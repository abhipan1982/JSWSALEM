const _Heat = new class {

  GoToHeat(heatId) {
    if (!heatId) {
      console.error('Missing data: heatId');
      return;
    }
    let dataToSend = {
      heatId: heatId
    };
    openSlideScreen('Heat', 'ElementDetails', dataToSend);
  }
}