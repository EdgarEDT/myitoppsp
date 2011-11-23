/***********************************************************************
 * Module:  DisplayMethods.cs
 * Author:  Administrator
 * Purpose: Definition of the Enum DisplayMethods
 ***********************************************************************/

using System;

public enum DisplayMethods
{
   /// Fields
   block = 1,
   compact = 4,
   inherit = 0x11,
   inline = 0,
   inline_table = 7,
   list_item = 2,
   marker = 5,
   none = 0x10,
   run_in = 3,
   table = 6,
   table_caption = 15,
   table_cell = 14,
   table_column = 13,
   table_column_group = 12,
   table_footer_group = 10,
   table_header_group = 9,
   table_row = 11,
   table_row_group = 8
}