﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetApi.Windows.Controls;
using YtAnalytics.Forms;
using YtCrawler;
using YtCrawler.Comments;

namespace YtAnalytics.Controls
{
	public delegate void AddCommentItemEventHandler(string item);
	public delegate void AddCommentEventHandler(Comment comment);

	/// <summary>
	/// A control displaying a list of video comments.
	/// </summary>
	public partial class ControlComments : ThreadSafeControl
	{
		private CommentsList comments;

		private FormAddComment formAdd = new FormAddComment();
		private FormComment formComment = new FormComment();

		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlComments()
		{
			InitializeComponent();
			// Default settings.
			this.Dock = DockStyle.Fill;
			this.Visible = false;
			this.formAdd.CommentAdded += new AddCommentEventHandler(this.OnCommentAdded);
		}

		/// <summary>
		/// Initializes the control.
		/// </summary>
		/// <param name="comments">A crawler object.</param>
		public void Initialize(CommentsList comments, Comment.CommentType commentType)
		{
			this.comments = comments;
			this.formAdd.CommentType = commentType;
			this.Enabled = true;

			switch (commentType)
			{
				case Comment.CommentType.Video: this.columnHeaderItem.Text = "Video"; break;
				case Comment.CommentType.User: this.columnHeaderItem.Text = "User"; break;
				case Comment.CommentType.Playlist: this.columnHeaderItem.Text = "Playlist"; break;
			}

			// Populate the comments list.
			foreach (Comment comment in this.comments)
			{
				// Add a new list view item.
				ListViewItem item = new ListViewItem(new string[] { comment.Time.ToString(), comment.Item, comment.User, comment.Text }, 0);
				item.Tag = comment;
				this.listView.Items.Add(item);
			}

			this.buttonExport.Enabled = this.listView.Items.Count > 0;
		}

		/// <summary>
		/// Opens the dialog to add a new comment.
		/// </summary>
		/// <param name="video">The video.</param>
		public void AddComment(string video)
		{
			this.formAdd.ShowDialog(this, video, Environment.UserName);
		}

		/// <summary>
		/// An event handler called when a new comment is added.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnAdd(object sender, EventArgs e)
		{
			this.formAdd.ShowDialog(this, string.Empty, Environment.UserName);
		}

		/// <summary>
		/// An event handler called when a comment is removed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRemove(object sender, EventArgs e)
		{
			// If there is no item selected, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Get the selected item.
			ListViewItem item = this.listView.SelectedItems[0];
			// Get the item comment.
			Comment comment = item.Tag as Comment;

			try
			{
				// Remove the comment.
				this.comments.RemoveComment(comment);
				// Remove the item.
				this.listView.Items.Remove(item);
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					string.Format("Cannot remove the comment. {0}", exception.Message),
					"Cannot Remove Comment",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			this.buttonExport.Enabled = this.listView.Items.Count > 0;
		}

		/// <summary>
		/// An event handler called when after new comment has been added.
		/// </summary>
		/// <param name="comment">The comment object.</param>
		private void OnCommentAdded(Comment comment)
		{
			try
			{
				// Add the comment to the comments list.
				this.comments.AddComment(comment);

				// Add a new list view item.
				ListViewItem item = new ListViewItem(new string[] { comment.Time.ToString(), comment.Item, comment.User, comment.Text }, 0);
				item.Tag = comment;
				this.listView.Items.Add(item);

				this.buttonExport.Enabled = true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					string.Format("Cannot add the comment. {0}", exception.Message),
					"Cannot Add Comment",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// An event handler called when the event selection has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnCommentSelectionChanged(object sender, EventArgs e)
		{
			if (this.listView.SelectedItems.Count != 0)
			{
				this.buttonRemove.Enabled = true;
				this.buttonView.Enabled = true;
				this.controlComment.Comment = this.listView.SelectedItems[0].Tag as Comment;
			}
			else
			{
				this.buttonRemove.Enabled = false;
				this.buttonView.Enabled = false;
				this.controlComment.Comment = null;
			}
		}

		/// <summary>
		/// An event handler called when the user mouse clicks the control.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (this.listView.FocusedItem != null)
				{
					if (this.listView.FocusedItem.Bounds.Contains(e.Location))
					{
						this.contextMenu.Show(this.listView, e.Location);
					}
				}
			}
		}

		/// <summary>
		/// An event handler called when the user opens a comment.
		/// </summary>
		/// <param name="sender">The sender control.</param>
		/// <param name="e">The event arguments.</param>
		private void OnViewComment(object sender, EventArgs e)
		{
			// If there are no selected items, do nothing.
			if (this.listView.SelectedItems.Count == 0) return;

			// Open a dialog with the selected comment.
			this.formComment.ShowDialog(this, this.listView.SelectedItems[0].Tag as Comment);
		}

		/// <summary>
		/// Imports comments from a specified file.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnImport(object sender, EventArgs e)
		{
			// If the user selects a file.
			if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					int countAdded;
					int countIgnored;
					// Try import the comments.
					ICollection<Comment> comments = this.comments.Import(this.openFileDialog.FileName, out countAdded, out countIgnored);
					// Add the comments to the list.
					if (null != comments)
					{
						// Populate the comments list.
						foreach (Comment comment in comments)
						{
							// Add a new list view item.
							ListViewItem item = new ListViewItem(new string[] { comment.Time.ToString(), comment.Item, comment.User, comment.Text }, 0);
							item.Tag = comment;
							this.listView.Items.Add(item);
						}
					}
					// Show a message.
					MessageBox.Show(
						string.Format("Import complete. {0} comments added, {1} comments ignored.", countAdded, countIgnored),
						"Import Complete",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				catch (Exception exception)
				{
					// Show a message.
					MessageBox.Show(
						string.Format("Import failed. {0}", exception.Message),
						"Import Failed",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
				this.buttonExport.Enabled = this.listView.Items.Count > 0;
			}
		}

		/// <summary>
		/// Exports comments to a specified file.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnExport(object sender, EventArgs e)
		{
			// If the user selects a file.
			if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				// Save the comments.
				this.comments.Save(this.saveFileDialog.FileName);
			}
		}
	}
}
