﻿#region License and Copyright
/*
 
Copyright (c) Guifreaks - Jacob Mesu
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the Guifreaks nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Guifreaks.Common
{
   public class LargeImageIndexEditor : UITypeEditor
   {
      object instance;

      public override bool GetPaintValueSupported(
         ITypeDescriptorContext context)
      {
         instance = context.Instance;
         return true;
      }

      public override void PaintValue(PaintValueEventArgs pe)
      {
         Image image = null;
         int imageIndex = 0;

         if (!int.TryParse(pe.Value.ToString(), out imageIndex))
            return;

         ImageList imageList = null;

         PropertyDescriptorCollection PropertyCollection
                           = TypeDescriptor.GetProperties(instance);

         PropertyDescriptor property;
         if ((property = PropertyCollection.Find("LargeImages", false)) != null)
            imageList = (ImageList)property.GetValue(instance);

         if ((imageList != null) && (imageList.Images.Count > imageIndex) && (imageIndex >= 0)) 
         {
            image = imageList.Images[imageIndex];
         }

         if (imageIndex < 0 || image == null)
         {
            pe.Graphics.DrawLine(Pens.Black, pe.Bounds.X + 1, pe.Bounds.Y + 1,
               pe.Bounds.Right - 1, pe.Bounds.Bottom - 1);
            pe.Graphics.DrawLine(Pens.Black, pe.Bounds.Right - 1, pe.Bounds.Y + 1,
               pe.Bounds.X + 1, pe.Bounds.Bottom - 1);
         }
         else
         {
            pe.Graphics.DrawImage(image, pe.Bounds);
         }
      }
   }
}
