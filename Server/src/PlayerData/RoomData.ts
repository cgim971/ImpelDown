export default class RoomData {

    private _roomIndex : number ; //들어가 잇는 방의 번호 들어가지 않을시 -1

    constructor() {
        this._roomIndex= -1;
    }

    setIsRoom(roomIndex:number = -1):void{
        this._roomIndex = roomIndex;
        return;
    }

    getIsRoom():boolean{
        return this._roomIndex > -1;
    }
}