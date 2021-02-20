<!DOCTYPE html>

<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
    
<html>
	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
		<title>INDEX2</title>
			
			<script src="./lib/jquery-3.5.1.min.js"  type="text/javascript"></script>
			<script src="./lib/jquery-ui.min.js"  type="text/javascript"></script>
		
			<link href="./src/skin-win8/ui.fancytree.css" rel="stylesheet">
			<script src="./src/jquery.fancytree.js"></script>
			<script src="./src/jquery.fancytree.filter.js"></script>
			
			<link rel="stylesheet" type="text/css" href="./common/css/format.css" />
			
			<link rel="stylesheet" href="//cdn.jsdelivr.net/npm/jquery-contextmenu@2.9.0/dist/jquery.contextMenu.min.css" />
  			<script src="//cdn.jsdelivr.net/npm/jquery-contextmenu@2.9.0/dist/jquery.contextMenu.min.js">
		
			<!-- Start_Exclude: This block is not part of the sample code -->
			<link href="./lib/prettify.css" rel="stylesheet">
			<script src="/lib/prettify.js"></script>
			<!-- <link href="sample.css" rel="stylesheet"> -->
			<!-- <script src="sample.js"></script> -->
				<!-- End_Exclude -->
		
		<script type="text/javascript">
		function fnSendFIleList() {
			window.external.ftpFileDownload($("#echoSelection3").text());
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
		
		//$(function(){
		$(document).ready(function(){
		
			$("#run_downloader").click(function() {
				fnSendFIleList();
			});
			
			 $("#btnDeselectAll3").click(function(){
				$.ui.fancytree.getTree("#tree").selectAll(false);
				return false;
			});
			$("#btnSelectAll3").click(function(){
				$.ui.fancytree.getTree("#tree").selectAll();
				return false;
			});
			/* $("#btnGetSelected3").click(function(){
				var selNodes = $.ui.fancytree.getTree("#tree").getSelectedNodes();
				var selData = $.map(selNodes, function(n){
					return n.toDict();
				});
				 alert(JSON.stringify(selData));
				return false;
			});  */
			
			$("#tree").fancytree({
				checkbox: true,
				extensions: ["filter"],
			    quicksearch: true,
				selectMode: 3,
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
						url : "/getFileList.do",
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
					data.tree.visit(function(n) {
						//n.key = n.title.split(" ")[0];
						n.expanded = true;
					});
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
				cookieId: "fancytree-Cb3",
				idPrefix: "fancytree-Cb3-"
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
			
				/* $.contextMenu({
			      selector: "#tree span.fancytree-title",
			      items: {
			        "cut": {name: "Cut", icon: "cut",
			            callback: function(key, opt){
			              var node = $.ui.fancytree.getNode(opt.$trigger);
			              alert("Clicked on " + key + " on " + node);
			            }
			          },
			        "copy": {name: "Copy", icon: "copy"},
			        "paste": {name: "Paste", icon: "paste", disabled: false },
			        "sep1": "----",
			        "edit": {name: "Edit", icon: "edit", disabled: true },
			        "delete": {name: "Delete", icon: "delete", disabled: true },
			        "more": {name: "More", items: {
			          "sub1": {name: "Sub 1"},
			          "sub1": {name: "Sub 2"}
			          }}
			        },
			      callback: function(itemKey, opt) {
			        var node = $.ui.fancytree.getNode(opt.$trigger);
			        alert("select " + itemKey + " on " + node);
			      }
			    }); */
		});
		</script>
	</head> 
	<body>
		<div id="layer_fixed">
		<p>
			&nbsp;&nbsp;&nbsp;&nbsp;
			<a href="#" id="btnSelectAll3">전체선택</a> -
			<a href="#" id="btnDeselectAll3">전체해제</a>
			<!-- <a href="#" id="btnGetSelected3">Get selected</a> -->
			&nbsp;&nbsp;&nbsp;&nbsp;
			<!--<span><a href="javascript:fnSendFIleList();">다운로드 실행열기</a></span>-->
			<span><input type="button"  id="run_downloader" style="background-color: green; font-weight: bold;  color:white" value="다운로드 실행"></span>
			
			<label>Filter:</label>
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
	</body>
</html>
