using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UOP.Agent
{
	public class ChatMessage : DeepSeekChatMessageModel, INotifyPropertyChanged
	{
		private string _content;
		[JsonProperty("content")]
		public new string Content
		{
			get { return _content; }
			set
			{
				_content = value;
				OnpropertyChanged();
			}
		}

		[JsonIgnore]
		public new string ChatRole
		{
			get { return _role == "user" ? "You:" : "DeepSeek:"; }
		}

		[JsonIgnore]
		public new string ChatMessageAlignment
		{
			get { return _role == "user" ? "Left" : "Right"; }
		}

		private string _role;
		[JsonProperty("role")]
		public new string Role
		{
			get { return _role; }
			set
			{
				_role = value;
				OnpropertyChanged();
			}
		}

		[JsonIgnore]
		public string IsUserBackground
		{
			get
			{
				if (_role == "user")
					return "#fafafa";
				else
					return "#f5f5f5";
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnpropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
