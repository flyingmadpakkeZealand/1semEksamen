﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.Magnus.Handler;

namespace _1SemEksamen.Magnus.Model
{
    public class Piano
    {
        public enum MusicalNoteNames
        {
            C,
            Cis,
            D,
            Dis,
            E,
            F,
            Fis,
            G,
            Gis,
            A,
            Ais,
            H
        }

        private StorageFolder _commonFolder = null;
        private List<MusicalNote> _musicalNotes;
        private List<Chord> _chords;
        private List<Melody> _melodies;
        private MediaElement _melodyPlayer;

        public Piano()
        {
            _musicalNotes = new List<MusicalNote>();
            _chords = new List<Chord>();
            _melodies = new List<Melody>();
            _melodyPlayer = new MediaElement();
            LoadFolderTask(); //Only allow constructor to return control if you have a working loading bar that "blocks" view until all tasks have completed.
        }

        private async void LoadFolderTask()
        {
            _commonFolder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            PianoVMHandler.ProgressBar = 33;
            Task test = Task.Run(() => //Simulated loading time.
            {
                Thread.Sleep(1000);
            });
            await test;
            SoundFiles.SoundFilesFolder = _commonFolder;

            _melodies.Add(new Melody("Melody1"));
            _melodies.Add(new Melody("Melody2"));
            _melodies.Add(new Melody("Melody3"));
            _melodies.Add(new Melody("Melody4"));

            _chords.Add(new Chord(new List<MusicalNote>(){new MusicalNote(MusicalNoteNames.C),new MusicalNote(MusicalNoteNames.E),new MusicalNote(MusicalNoteNames.G)}));
            _chords.Add(new Chord(new List<MusicalNote>() { new MusicalNote(MusicalNoteNames.A), new MusicalNote(MusicalNoteNames.C), new MusicalNote(MusicalNoteNames.E) }));
            _chords.Add(new Chord(new List<MusicalNote>() { new MusicalNote(MusicalNoteNames.G), new MusicalNote(MusicalNoteNames.H), new MusicalNote(MusicalNoteNames.D) }));

            for (int i = 0; i < 12; i++)
            {
                _musicalNotes.Add(new MusicalNote((MusicalNoteNames)i));
            }

            PianoVMHandler.ProgressBar = PianoVMHandler.ProgressBar + 1;
        }
        /*
         Allowing your default constructor to return control creates a whole bunch of problems, luckily it seems that some of these can be prevented
         by updating relevant properties in the constructor (if you suspect it to return control before these properties have a valid state).
         Also, constructors cannot be async which means that it is important to group together things that are async by nature, but also have a certain
         order they must follow, inside an async method, see LoadFolderTask or async methods in MusicalNote.
         */

        public List<Melody> Melodies
        {
            get { return _melodies; }
        }

        public void PlayPianoNote(int noteToPlay)
        {
            _musicalNotes[noteToPlay].PlayNote();
        }

        public void PlayPianoChord(int chordToPlay)
        {
            _chords[chordToPlay].PlayChord();
        }

        public void PlayPianoMelody(int melodyToPlay)
        {
            _melodyPlayer.Stop();
            _melodyPlayer = _melodies[melodyToPlay].OneMelody;
            _melodyPlayer.Play();
        }
    }
}
