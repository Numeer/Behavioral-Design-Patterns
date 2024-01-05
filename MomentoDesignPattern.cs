/*Need to restore an object back to its previous state (e.g. "undo" or "rollback" operations).
The Memento captures and externalizes an object's internal state 
so that the object can later be restored to that state
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{

    // Memento: Stores the state of the TextEditor
    class TextEditorMemento
    {
        public string Text { get; }

        public TextEditorMemento(string text)
        {
            Text = text;
        }
    }

    // Originator: Creates a memento containing a snapshot of its current state
    class TextEditor
    {
        private string text;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                Console.WriteLine("Text set: " + text);
            }
        }

        public TextEditorMemento Save()
        {
            return new TextEditorMemento(text);
        }

        public void Restore(TextEditorMemento memento)
        {
            text = memento.Text;
            Console.WriteLine("Restored text: " + text);
        }
    }

    // Caretaker: Manages multiple mementos
    class TextEditorHistory
    {
        public TextEditorMemento Memento { get; set; }
    }

    // Memento: Stores the state of the ChessBoard
    class ChessBoardMemento
    {
        public List<string> Pieces { get; }

        public ChessBoardMemento(List<string> pieces)
        {
            Pieces = new List<string>(pieces);
        }
    }

    // Originator: Creates a memento containing a snapshot of its current state
    class ChessBoard
    {
        private List<string> pieces;

        public ChessBoard(List<string> initialPieces)
        {
            pieces = new List<string>(initialPieces);
            Console.WriteLine("Initial board setup:");
            PrintBoard();
        }

        public void MovePiece(int fromIndex, int toIndex)
        {
            // Logic to move a chess piece
            pieces[toIndex] = pieces[fromIndex];
            pieces[fromIndex] = ".";
            Console.WriteLine($"Moved piece from {fromIndex} to {toIndex}:");
            PrintBoard();
        }

        public ChessBoardMemento Save()
        {
            return new ChessBoardMemento(pieces);
        }

        public void Restore(ChessBoardMemento memento)
        {
            pieces = new List<string>(memento.Pieces);
            Console.WriteLine("Restored board:");
            PrintBoard();
        }

        private void PrintBoard()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                Console.Write(pieces[i] + " ");
                if ((i + 1) % 8 == 0)
                    Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    // Caretaker: Manages multiple mementos
    class ChessGameHistory
    {
        public ChessBoardMemento Memento { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            TextEditor textEditor = new TextEditor();
            TextEditorHistory history1 = new TextEditorHistory();

            // Initial state
            textEditor.Text = "First line";
            history1.Memento = textEditor.Save();

            // Change state
            textEditor.Text = "Second line";

            // Restore to previous state
            textEditor.Restore(history1.Memento);

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            List<string> initialBoard = new List<string>
            {
                "R", "N", "B", "Q", "K", "B", "N", "R",
                "P", "P", "P", "P", "P", "P", "P", "P",
                ".", ".", ".", ".", ".", ".", ".", ".",
                ".", ".", ".", ".", ".", ".", ".", ".",
                ".", ".", ".", ".", ".", ".", ".", ".",
                ".", ".", ".", ".", ".", ".", ".", ".",
                "p", "p", "p", "p", "p", "p", "p", "p",
                "r", "n", "b", "q", "k", "b", "n", "r"
            };

            ChessBoard chessBoard = new ChessBoard(initialBoard);
            ChessGameHistory history = new ChessGameHistory();

            // Save initial state
            history.Memento = chessBoard.Save();

            // Make some moves
            chessBoard.MovePiece(1, 18); // Moving a pawn
            chessBoard.MovePiece(57, 42); // Moving a pawn

            // Restore to initial state
            chessBoard.Restore(history.Memento);

        }
    }

}

