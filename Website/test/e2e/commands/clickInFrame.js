exports.command = function (frameSelector, buttonSelector, waitTime) {
	if (arguments.length < 3 && typeof buttonSelector !== 'string') {
		if (typeof buttonSelector === 'number') {
			waitTime = buttonSelector;
		}
		buttonSelector = frameSelector;
	}

	this.selectFrame(frameSelector);
	this.waitForElementVisible(buttonSelector, waitTime || this.api.globals.timeouts.basic);
	this.click(buttonSelector);
	if (waitTime) {
		this.pause(waitTime);
	}
  return this;
};
