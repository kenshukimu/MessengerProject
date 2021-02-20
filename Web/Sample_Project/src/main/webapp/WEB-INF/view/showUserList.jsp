<!DOCTYPE html>

<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
    
<html>
	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<title>INDEX2</title>
			
			<script src="./lib/jquery-3.5.1.min.js"  type="text/javascript"></script>
			<script src="./lib/jquery-ui.min.js"  type="text/javascript"></script>
			
			<script src="./src/jquery.popupoverlay.min.js"></script>
					
			<link href="./src/skin-win8/ui.fancytree.css" rel="stylesheet">
			<script src="./src/jquery.fancytree.js"></script>
			<script src="./src/jquery.fancytree.filter.js"></script>
			
			<link rel="stylesheet" type="text/css" href="./common/css/format.css" />	
			
			<link rel="stylesheet" href="//cdn.jsdelivr.net/npm/jquery-contextmenu@2.9.0/dist/jquery.contextMenu.min.css" />
  			<script src="//cdn.jsdelivr.net/npm/jquery-contextmenu@2.9.0/dist/jquery.contextMenu.min.js">
		
			<!-- Start_Exclude: This block is not part of the sample code -->
			<link href="./lib/prettify.css" rel="stylesheet">
			<script src="/lib/prettify.js"></script>
				<!-- End_Exclude -->
		<style type="text/css">
			#popup-content { display:none; text-align:center}
		
	span.fancytree-node.customOnline > span.fancytree-title {
		color: blue;
        font-size:13px;
        font-weight:bold;
	}
	
	span.fancytree-node.customOffline > span.fancytree-title {
		color: black;
        font-size:13px
	}
/* 	span.fancytree-node.custom1 > span.fancytree-icon {
		 height: 16px;
  		 width: 16px;
  		background-image: url("/common/images/group.png");
	} */
		</style>
		
		<script type="text/javascript">	
		function fnSendRunApp(itemKey) {
			window.external.RunApp($("#selProfileId").text(),
										   $("#echoSelection3").text(),
										   itemKey);
		}
		
		function fnOnLoding(value) {			
			$(".loading").show();
		}
		
		function fnOffLoding(value) {
			$(".loading").hide();
		}
		
		function checkNodesForChildren(nodes) {
	         $.each(nodes, function (index, node) {
	            if (node.children) {
	               node.folder = true;
	               checkNodesForChildren(node.children);
	            }
	         });
	      }
		
		//온라인 처리
		function onLineMark(users) {	
			var jbSplit = users.split('^');
		      for ( var i in jbSplit ) {
		    	  if(jbSplit[i].length < 1) continue;
		    	  var tree = $.ui.fancytree.getTree("#tree"),
					 node = tree.getNodeByKey( jbSplit[i]);
					 node.extraClasses = "customOnline";
					 node.icon = "custom-2";
					 node.renderTitle();
		      }			
		}
		
		//오프라인 처리
		function offLineMark(users) {			
			var jbSplit = users.split('^');
		      for ( var i in jbSplit ) {
		    	  if(jbSplit[i].length < 1) continue;
		    	  var tree = $.ui.fancytree.getTree("#tree"),
					 node = tree.getNodeByKey(jbSplit[i]);
					 node.extraClasses = "customOffline";
					 node.icon = "custom-3";
					 node.renderTitle();
		      }			
		}
		
		//$(function(){
		$(document).ready(function(){	
			
			 $("#btnDeselectAll3").click(function(){
				$.ui.fancytree.getTree("#tree").selectAll(false);
				return false;
			});
			$("#btnSelectAll3").click(function(){
				$.ui.fancytree.getTree("#tree").selectAll();
				return false;
			});
			
			$("#tree").fancytree({				
				activate: function(event, data) {
			       var node = data.node;
			       $("#echoActivated").text(node.title + ", key=" + node.key);
			    },
				checkbox: true,
				extensions: ["filter"],
			    quicksearch: true,
				selectMode: 3,
				
				/* icon: function(event, data) {
			        // For the sake of this example set specific icons in different ways.
			        //
			        switch( data.node.type ) {
			          case "F":
			        	  return "custom-2";
			          case "D":
			            // Insert an <i> tag that will be replaced with an inline SVG graphic
			            // by Font Awesome's all.js library.
			            // Note: We DON'T want this, since it will be slow for large trees!
			        	  return "custom-1";
			        }
			    }, */
				
				filter: {
			        autoApply: true,   // Re-apply last filter if lazy data is loaded
			        autoExpand: false, // Expand all branches that contain matches while filtered
			        counter: true,     // Show a badge with number of matching child nodes near parent icons
			        fuzzy: false,      // Match single characters in order, e.g. 'fb' will match 'FooBar'
			        hideExpandedCounter: true,  // Hide counter badge if parent is expanded
			        hideExpanders: false,       // Hide expanders if all child nodes are hidden by filter
			        highlight: true,   // Highlight matches by wrapping inside <mark> tags
			        leavesOnly: false, // Match end nodes only
			        nodata: true,      // Display a 'no data' status node if result is empty
			        mode: "dimm"       // Grayout unmatched nodes (pass "hide" to remove unmatched node instead)
			      },
				  source: function(event, data) {
					$.ajax({
						type : "POST",
						url : "/getUserList.do",
						data: {},
						dataType: "json",
						async:false,
				        contentType: "application/json",
						success : function(files) {
							 if(files.msg=="OK") {
								var obj = files.list;				
								data.result = obj;
							}  
						}
					});
					//alert(JSON.stringify(data.result))
					return data.result;
				},				
				init: function(event, data) {
					$("#loadingbar").attr("src", "");
					$("#lodingdiv").hide();
					

					// Set key from first part of title (just for this demo output)
					/* data.tree.visit(function(n) {
						//n.key = n.title.split(" ")[0];
						n.expanded = true;
					}); */
				},				
				lazyLoad: function(event, ctx) {
					ctx.result = {url: "ajax-sub2.json", debugDelay: 1000};
				},
				loadChildren: function(event, ctx) {
					ctx.node.fixSelection3AfterClick();					
				},
     			postProcess: function (event, data) {
		            checkNodesForChildren(data.response);
		        },
				select: function(event, data) {
					// Get a list of all selected nodes, and convert to a key array:
					var selKeys = $.map(data.tree.getSelectedNodes(), function(node){
						return node.key;
					});
					$("#echoSelection3").text(selKeys.join("^"));

					// Get a list of all selected TOP nodes
					//var selRootNodes = data.tree.getSelectedNodes(true);
					// ... and convert to a key array:
					//var selRootKeys = $.map(selRootNodes, function(node){
					//	return node.key;
					//});
					//$("#echoSelectionRootKeys3").text(selRootKeys.join(", "));
					// $("#echoSelectionRoots3").text(selRootNodes.join(", "));
				},
				// The following options are only required, if we have more than one tree on one page:
				//cookieId: "fancytree-Cb3",
				//idPrefix: "fancytree-Cb3-",	
			});
			
			var tree = $.ui.fancytree.getTree("#tree");
			
			$("button#btnResetSearch").click(function(e){
			      $("input[name=search]").val("");
			      $("span#matches").text("");
			      tree.clearFilter();
			    }).attr("disabled", true);
			
			$("input[name=search]").on("keyup", function(e){
			      var n,
			        tree = $.ui.fancytree.getTree(),
			        args = "autoApply autoExpand fuzzy hideExpanders highlight leavesOnly nodata".split(" "),
			        opts = {},
			        filterFunc = $("#branchMode").is(":checked") ? tree.filterBranches : tree.filterNodes,
			        match = $(this).val();

			      $.each(args, function(i, o) {
			        opts[o] = $("#" + o).is(":checked");
			      });
			      opts.mode = $("#hideMode").is(":checked") ? "hide" : "dimm";
			      if(e && e.which === $.ui.keyCode.ESCAPE || $.trim(match) === ""){
			        $("button#btnResetSearch").click();
			        return;
			      }
			      if($("#regex").is(":checked")) {
			        // Pass function to perform match
			        n = filterFunc.call(tree, function(node) {
			          return new RegExp(match, "i").test(node.title);
			        }, opts);
			      } else {
			        // Pass a string to perform case insensitive matching
			        n = filterFunc.call(tree, match, opts);
			      }
			      $("button#btnResetSearch").attr("disabled", false);
			      $("span#matches").text("(" + n + " matches)");
			    }).focus();
			
				$.contextMenu({
			      selector: "#tree span.fancytree-title",
			      items: {
			     	"sendMessage": {name: "메세지보내기", icon: "mail"},
			        "viewUserInfo": {name: "사용자 정보보기", icon: "info" },
			        "sendChat": {name: "채팅하기", icon: "chat" },
			        /* 
			        "sep1": "----",
			        "edit": {name: "Edit", icon: "edit", disabled: true },
			        "delete": {name: "Delete", icon: "delete", disabled: true },
			        "more": {name: "More", items: {
			        "sub1": {name: "Sub 1"},
			        "sub2": {name: "Sub 2"} 
			          }}
			        */
			        },
				      callback: function(itemKey, opt) {
				        var node = $.ui.fancytree.getNode(opt.$trigger);
				        //alert("select " + itemKey + " on " + node.key);
				        $("#selProfileId").text(node.key);
				        if(node.type == "F") {        
				        	fnSendRunApp(itemKey);
				        }else{
				        	alert("그룹에서 개별메뉴 선택은 불가합니다");
				        }
			      }
			    });
		});	
	
		</script>
	</head> 
	<body>
		<div id="layer_fixed">
		<p>
			<!-- 	
			&nbsp;&nbsp;&nbsp;&nbsp;
			<a href="#" id="btnSelectAll3">전체선택</a> -
			<a href="#" id="btnDeselectAll3">전체해제</a>
			<a href="#" id="btnGetSelected3">Get selected</a>
			&nbsp;&nbsp;&nbsp;&nbsp; -->
			
			<label style="color: white">사용자찾기:</label>
    		<input name="search" placeholder="Filter..." autocomplete="off">
    			<button id="btnResetSearch">&times;</button>
    		<span id="matches"></span>
		</p>
		</div>	
		<div id="layer_body">
			<div class="flex-container" id="lodingdiv"> 
					<div> 
						<img alt="" src="./img/LoadingBar.jpg" id="loadingbar"  />
					</div> 
			</div>	 	
		 	<div id="tree" >
		 	</div>
			<input type="hidden" id="echoSelection3" />		
		 </div>
		 
		<!-- <button id="btn_showProfile" class="profile_open" >Click me</button>
		<button id="btn_showProfile2" class="profile_open2" >Click me2</button> -->
		<!-- <section id="profile">
  			<div id="card" ></div>
		</section>
		-->	
		<input type="hidden" id="selProfileId" />
	</body>
</html>
