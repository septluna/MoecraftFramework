//var fso = new ActiveXObject(Scripting.FileSystemObject);
//var file = fso.createtextfile;
var editWindow = $('#edit_window');


/**
 * 代码高亮
 */

function highLight(){
	
}



/**
 * 查错
 */

function syntaxError(){
	
}



/**
 * 自动补全代码
 */

function autoComplete (keyCode){
	switch (keyCode){
		case ''
	}
}



/**
 * 注册事件
 */

editWindow.on('keydown', function(event){
	event = event || window.event;
	autoComplete(event.keyCode)
})
