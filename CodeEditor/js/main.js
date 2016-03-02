var editWindow = $('#edit_window');
var funcNum = rules.function.length;


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
var listenInput = false;

function autoComplete (){
	
	//在行首按下"<"
	if (getForwardWord(2) == '\n<' || (getCursorPos() == 1 && getForwardWord() == '<')){
		if (listenInput == false){
			listenInput = true;
			window.startPos = getCursorPos();
		}
	}

	//监听输入
	if (listenInput == true){
		for (var i = 0; i < funcNum; i++){
			//在第一位找到了关键字
			if (getForwardWord(getCursorPos() - startPos).indexOf(rules.function[i].name) == 0){
				for (var j = 0; j < rules.function[i].attr.length; j++){
					insertStr(' ' + rules.function[i].attr[j] + '=""');
				}
				insertStr('>');
				
				listenInput = false;
			}
		}	
	}
}


/**
 * 获取光标在短连接输入框中的位置
 * @param inputId 框Id
 * @return {*}
 */
function getCursorPos(inputId){
	if (inputId === undefined) inputId = 'edit_window';
	
	var inpObj = document.getElementById(inputId);
	if(navigator.userAgent.indexOf("MSIE") > -1) { // IE
		var range = document.selection.createRange();
		range.text = '';
		range.setEndPoint('StartToStart',inpObj.createTextRange());
		return range.text.length;
	} else {
		return inpObj.selectionStart;
	}
}


/**
 * 获取光标前的字符
 */
function getForwardWord(length){
	if (length === undefined) length = 1; // 缺省取1位
	return editWindow.val().substr(getCursorPos() - length, length);
}

/**
 * 在光标位置插入字符串
 */
function insertStr(str){
	editWindow.val(editWindow.val().substr(0, getCursorPos()) + str + editWindow.val().substr(getCursorPos()))
}

/**
 * 注册事件
 */

editWindow.on('input', function(){
	autoComplete();
})
