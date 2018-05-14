using System;
using i = IrProject.Indexing;
using d = IrProject.Data;

namespace IrProject.ConsoleUtils
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            string source = i.Helper.PAGES_PATH;
            string target = i.Helper.DOCS_PATH;
            i.TagRemover tr = new i.TagRemover(source, target);
            tr.RemoveTags();
            i.TitleExtractor te = new i.TitleExtractor();
            te.Extract(i.Helper.PAGES_PATH);
            i.LinkProcessor lp = new i.LinkProcessor();
            lp.Run();
            i.DataHelper dh = new i.DataHelper();
            dh.AddTitleUrlTags();
            i.TermParser p = new i.TermParser();
            p.ExtractTerms();

            i.Calculator c = new i.Calculator();
            c.CalculateIdfs();
            c.CalculateIdfsA();

            i.AnchorTextProcessor at = new IrProject.Indexing.AnchorTextProcessor();
            at.AddAnchorText();

            Tester t = new Tester();
            t.Run();

            TextConverter tc = new TextConverter();
            tc.ConvertTermDocTable();
        }
    }
}