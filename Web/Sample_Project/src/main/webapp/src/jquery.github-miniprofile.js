(function ($) {
	
	$.githubProfile = {
			
		defaults: {
			user:		'',
			fields:		[],
			perLine:	0,
			width:		350,
			color:		'blue'
		},
		init: function(me, options) {
			opts = $.extend({}, $.githubProfile.defaults, options);
			opts.perLine = Math.min(opts.perLine, opts.fields.length);
			$.githubProfile.getUser(me, opts);
		},
		
		constructHtml: function(opts, data) { 
			var html = '<div class="ghProfile ghProfile-' + opts.color + '" style="width: ' + opts.width + 'px;">';
			html += '<div class="ghProfile-top">';
			html += '<table><tr>';
			html += '<td><a href="' + data.html_url + '"><img src="' + data.avatar_url + '" /></a></td><td>';
			html += (data.name ? '<h1>' + data.name + '</h1>' : '') + '<h2>' + data.login + '</h2>' + (opts.company && data.company ? '<h3>' + data.company + '</h3>' : '');
			html += opts.blog && data.blog ? '<a href="' + data.blog + '">' + data.blog + '</a>' : '';
			html += '</td></tr></table>';
			html += '</div>';
			return html + '</div>';
		},
		
		getUser: function(me, opts) {
			$.get('https://api.github.com/users/' + opts.user, function(data) {
				$(me).html($.githubProfile.constructHtml(opts, data));
			});
		}
	};

    jQuery.fn.githubProfile = function(options) {    	
    	$.githubProfile.init(this, options);
    };

}( jQuery ));