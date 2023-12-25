namespace MyFirstFramework
{
   public static class RandomExtensions
   {
      public static ConditionalAction If(this Random random, Func<Random, bool> pred) {

         bool result = pred(random);
         return new ConditionalAction(result);
      }

      public class ConditionalAction
      {
         private readonly bool conditionResult;

         public ConditionalAction(bool result)
         {
            this.conditionResult = result;
         }

         public void Then(Action ifTrue, Action ifFalse)
         {
            if (conditionResult)
               ifTrue();
            else
               ifFalse();
         }
      }
   }
}
