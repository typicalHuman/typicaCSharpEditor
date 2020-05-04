﻿using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace typicalIDE.CodeBox.Completions.CSharpCompletion
{
    public class CSharpCompletion: INotifyPropertyChanged ,ICompletionData
    {

        public CSharpCompletion(string text)
        {
            this.Text = text;
        }

        public CSharpCompletion(string text, CompletionTypes type)
        {
            _image = CompletionImage.GetImageSource(type);
            this.Text = text;
        }

        private static IEnumerable<CSharpCompletion> GetCompletions(params string[] keyWords)
        {
            IList<CSharpCompletion> list = new List<CSharpCompletion>();
            for (int i = 0; i < keyWords.Length; i++)
                yield return new CSharpCompletion(keyWords[i]);
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
        }

        public string Text { get; private set; }

        public object Content
        {
            get { return this.Text; }
        }

        private Brush selectionColor = Brushes.Transparent;
        public Brush SelectionColor
        {
            get => selectionColor;
            set
            {
                selectionColor = value;
                OnPropertyChanged("SelectionColor");
            }
        }

        public object Description
        {
            get { return "Keyword: " + this.Text; }
        }

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment.Offset, completionSegment.Length, 
                Text, OffsetChangeMappingType.CharacterReplace);
        }

        public double Priority { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}