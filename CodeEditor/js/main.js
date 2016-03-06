var editWindow = $('#edit_window');
var _editWindow = document.getElementById('edit_window');
var funcNum = rules.function.length;


/**
 * 代码高亮
 */

function highLight() {

}



/**
 * 查错
 */

function syntaxError() {

}



/**
 * 自动补全代码
 */
var isHintShow = false;
var listenInput = false;
var inputStr = '';
var startPos;
var functionsName = '';
for (var i = 0; i < funcNum; i++){
	functionsName += rules.function[i].name;
}
var hintList = [];
var findInList = false;
var lastCurPos;


function autoComplete() {
	
	//设定要截取的字符数量（针对中文输入法会一次性输出多个字符的情况）
	var wordLen = getCursorPos() - lastCurPos; 
	
	
	//第一个字符存在于关键字中
	if (getForwardWord().length > 0 && functionsName.indexOf(getForwardWord(wordLen)) > -1 && !findInList){
		for (var i = 0, j = 0; i < funcNum; i++) {
			if (rules.function[i].name.indexOf(getForwardWord()) > -1){
				hintList[j] = rules.function[i].name;
				j++;
			}
		}
		findInList = true;
	}
	
	
	//删除提示列表中不匹配的项
	if (findInList){
		for (var i = 0; i < hintList.length; i++) {
			if (hintList[i].indexOf(getForwardWord(wordLen)) == -1){
				hintList.splice(i, 1);
				i = -1;
			}
		}
	}



	//显示提示框
	if (hintList.length > 0){
		if (getCurCordinate().left + $('#code_hinting').width() - 10 > $(window).width() - ($(window).width() - editWindow.width()) / 2){
			$('#code_hinting').css({
			'visibility': 'visible',
			'opacity': '1',
			'top': getCurCordinate().top + 25,
			'left': $(window).width() - ($(window).width() - editWindow.width()) / 2 - $('#code_hinting').width() - 10
			});
		}
		else {
			$('#code_hinting').css({
			'visibility': 'visible',
			'opacity': '1',
			'top': getCurCordinate().top + 25,
			'left': getCurCordinate().left - 20
			});
		}
		
		var hintVal = '';
		for (var i = 0; i < hintList.length; i++){
			hintVal += hintList[i] + '<br />';
		}
		
		$('#code_hinting').html(hintVal)
	}
	// 无匹配时隐藏提示框
	else {
		$('#code_hinting').css(
			'visibility', 'hidden'
		);
		findInList = false;
	}
	
	
	
	
	for (var i = 0; i < funcNum; i++){
		//前一个字符存在于关键字中
		if (rules.function[i].name.indexOf(getForwardWord()) == 0 && !listenInput){
			
		}
		
		//监听其他输入
		if (listenInput){
			inputStr += editWindow.val().substring(startPos, getCursorPos());
			console.log(inputStr)
		}
		
		
		
		//匹配到关键字
		if (getForwardWord(rules.function[i].name.length).indexOf(rules.function[i].name) != -1 && !isHintShow){
			isHintShow = true;
		}
	}
	
	
	
	//记录光标位置
	lastCurPos = getCursorPos();

	
	/**
	 * TODO: 鼠标点击其他位置时，隐藏提示框、清除光标位置记录、findInList归false、清空列表…… 
	 */
}



/**
 * 注册事件
 */

editWindow.on('input', function() {
	autoComplete();
})