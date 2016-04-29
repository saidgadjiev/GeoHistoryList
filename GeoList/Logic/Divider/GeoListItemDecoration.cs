using System;
using Android.Support.V7.Widget;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Content;
using Android.Graphics;
using Android.Views;

namespace GeoList
{
	public class GeoListItemDecoration: RecyclerView.ItemDecoration
	{
		private static readonly int[] ATTRS = new int[]{
			Android.Resource.Attribute.ListDivider
		};

		public static readonly int HORIZONTAL_LIST = LinearLayoutManager.Horizontal;

		public static readonly int VERTICAL_LIST = LinearLayoutManager.Vertical;

		private Drawable mDivider;

		private int mOrientation;

		public GeoListItemDecoration(Context context, int orientation) {
			TypedArray a = context.ObtainStyledAttributes(ATTRS);
			mDivider = a.GetDrawable(0);
			a.Recycle();
			setOrientation(orientation);
		}

		public void setOrientation(int orientation) {
			if (orientation != HORIZONTAL_LIST && orientation != VERTICAL_LIST) {
				throw new NotImplementedException("invalid orientation");
			}
			mOrientation = orientation;
		}

		public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state) {
			if (mOrientation == VERTICAL_LIST) {
				drawVertical(c, parent);
			} else {
				drawHorizontal(c, parent);
			}
		}

		public void drawVertical(Canvas c, RecyclerView parent) {
			int left = parent.PaddingLeft;
			int right = parent.Width - parent.PaddingRight;

			int childCount = parent.ChildCount;
			for (int i = 0; i < childCount; i++) {
				View child = parent.GetChildAt(i);
				RecyclerView.LayoutParams parameters = (RecyclerView.LayoutParams)child.LayoutParameters;
				int top = child.Bottom + parameters.BottomMargin;
				int bottom = top + mDivider.IntrinsicHeight;
				mDivider.SetBounds(left, top, right, bottom);
				mDivider.Draw(c);
			}
		}

		public void drawHorizontal(Canvas c, RecyclerView parent) {
			int top = parent.PaddingTop;
			int bottom = parent.Height - parent.PaddingBottom;

			int childCount = parent.ChildCount;
			for (int i = 0; i < childCount; i++) {
				View child = parent.GetChildAt(i);
				RecyclerView.LayoutParams parameters = (RecyclerView.LayoutParams) child.LayoutParameters;
				int left = child.Right + parameters.RightMargin;
				int right = left + mDivider.IntrinsicHeight;
				mDivider.SetBounds(left, top, right, bottom);
				mDivider.Draw(c);
			}
		}

		public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state) {
			if (mOrientation == VERTICAL_LIST) {
				outRect.Set(0, 0, 0, mDivider.IntrinsicHeight);
			} else {
				outRect.Set(0, 0, mDivider.IntrinsicWidth, 0);
			}
		}
	}
}

