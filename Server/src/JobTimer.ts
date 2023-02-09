export default class JobTimer {
    action: () => void;
    private _time: number = 0;
    private _intervalTimer?: NodeJS.Timer;

    constructor(_time: number, action: () => void) {
        this._time = _time;
        this.action = action;
    }

    stopTimer(): void {
        clearInterval(this._intervalTimer);
    }

    startTimer(): void {
        this._intervalTimer = setInterval(this.action, this._time);
    }
}